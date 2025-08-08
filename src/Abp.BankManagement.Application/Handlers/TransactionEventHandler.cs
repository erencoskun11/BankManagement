using Abp.BankManagement.Etos.TransactionDtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.EventHandlers
{
    public class TransactionEventHandler :
         IDistributedEventHandler<TransactionCreateEto>,
         IDistributedEventHandler<TransactionUpdateEto>,
         IDistributedEventHandler<TransactionEto>
    {
        private readonly ILogger<TransactionEventHandler> _logger;

        public TransactionEventHandler(ILogger<TransactionEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleEventAsync(TransactionCreateEto eventData)
        {
            _logger.LogInformation($"[Transaction Created] Amount: {eventData.Amount}, Description: {eventData.Description}");
            return Task.CompletedTask;
        }

        public Task HandleEventAsync(TransactionUpdateEto eventData)
        {
            _logger.LogInformation($"[Transaction Updated] Amount: {eventData.Amount}, Description: {eventData.Description}");
            return Task.CompletedTask;
        }

        public Task HandleEventAsync(TransactionEto eventData)
        {
            _logger.LogInformation($"[Transaction Listed] Amount: {eventData.Amount}, Date: {eventData.TransactionDate}");
            return Task.CompletedTask;
        }
    }
}
