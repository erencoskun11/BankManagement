using Abp.BankManagement.Etos.CustomerEtos;
using Abp.BankManagement.Etos.TransactionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Publishers
{
    public class TransactionEventPublisher : ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public TransactionEventPublisher(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        public async Task PublishTransactionCreatedAsync(TransactionCreateEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

        public async Task PublishTransactionListedAsync(TransactionEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

        public async Task PublishTransactionUpdatedAsync(TransactionUpdateEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

       
    }
}
