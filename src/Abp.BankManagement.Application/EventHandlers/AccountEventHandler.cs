/*using Abp.BankManagement.Etos.AccountEtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.EventHandlers
{
    public class AccountEventHandler : 
        IDistributedEventHandler<AccountCreatedEto>,
        IDistributedEventHandler<AccountUpdatedEto>,
        IDistributedEventHandler<AccountEto>
    {
        private readonly ILogger<AccountEventHandler> _logger;

        public AccountEventHandler(ILogger<AccountEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleEventAsync(AccountCreatedEto eventData)
        {
            _logger.LogInformation($"[Account Created] Name : {eventData.AccountName}, IBAN: {eventData.IBAN}");
            return Task.CompletedTask;
        }

        public Task HandleEventAsync(AccountUpdatedEto eventData)
        {
            _logger.LogInformation($"[Account Updated] Name: {eventData.AccountName}, Active: {eventData.IsActive}");
            return Task.CompletedTask;

        }

        Task IDistributedEventHandler<AccountEto>.HandleEventAsync(AccountEto eventData)
        {
            _logger.LogInformation($"[Account Listed] Name: {eventData.AccountName}, Number: {eventData.AccountNumber}");
            return Task.CompletedTask;
        }
    }
}
*/