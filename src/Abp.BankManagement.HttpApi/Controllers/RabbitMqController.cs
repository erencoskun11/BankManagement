using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Volo.Abp.AspNetCore.Mvc;
using Abp.BankManagement.Application.Services;

namespace Abp.BankManagement.HttpApi.Controllers
{
    [ApiController]
    [Route("api/rabbitmq")]
    public class RabbitMqController : AbpControllerBase
    {
        private readonly IRabbitMqConnectionService _connectionService;

        public RabbitMqController(IRabbitMqConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public class QueueCreateRequest
        {
            public string QueueName { get; set; }
        }

        [HttpPost("queue")]
        public IActionResult CreateQueue([FromBody] QueueCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.QueueName))
                return BadRequest("QueueName is required.");

            var connection = _connectionService.GetConnection();
            using var channel = connection.CreateModel(); // IConnection interface'inden gelir
            channel.QueueDeclare(
                queue: request.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            return Ok($"Queue '{request.QueueName}' created.");
        }
    }
}
