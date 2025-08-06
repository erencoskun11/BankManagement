using Abp.BankManagement.Etos.CustomerEtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.EventHandlers
{
    public class CustomerCreateEventHandler : IDistributedEventHandler<CustomerCreateEto>
    {
        private readonly ILogger _logger;

        public CustomerCreateEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task HandleEventAsync(CustomerCreateEto eventData)
        {
            _logger.LogInformation($"[Customer Created] Name: {eventData.FullName}, NationalId: {eventData.NationalId}");
            return Task.CompletedTask;
        }
    }
}
