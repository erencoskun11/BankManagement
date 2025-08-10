using RabbitMQ.Client;

namespace Abp.BankManagement.Application.Services
{
    public interface IRabbitMqConnectionService
    {
        IConnection GetConnection();
    }
}
