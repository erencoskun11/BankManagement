
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Volo.Abp.DependencyInjection;

namespace Abp.BankManagement.Application.Services
{
    public interface IRabbitMqService : ITransientDependency
    {
        Task SendMessageAsync(string queueName, object message);
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;

        public RabbitMqService(IRabbitMqConnectionService connectionService)
        {
            _connection = connectionService.GetConnection();
        }

        public async Task SendMessageAsync(string queueName, object message)
        {
            using var channel = _connection.CreateModel();

            // Kuyruğu oluşturuyoruz, yoksa hata vermesin diye
            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true; // Mesaj kalıcı olsun

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: properties,
                                 body: body);

            await Task.CompletedTask;
        }
    }
}
