using Abp.BankManagement.Etos.AccountEtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Publishers
{
    public class AccountEventPublisher : ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public AccountEventPublisher(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        public async Task PublishAccountListedAsync(AccountEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

        public async Task PublishAccountCreatedAsync(AccountCreatedEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

        public async Task PublishAccountUpdatedAsync(AccountUpdatedEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }
    }
}
