using Abp.BankManagement.Etos.CardEtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Publishers
{
    public class CardEventPublisher: ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public CardEventPublisher(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        public async Task PublicCardCreateAsync(CardCreateEto cardCreateEto)
        {
            await _distributedEventBus.PublishAsync(cardCreateEto);
        }

        public async Task PublishCardListedAsync(CardEto cardEto)
        {
            await _distributedEventBus.PublishAsync(cardEto);
        }

        public async Task PublishCardUpdatedAsync(CardUpdateEto cardUpdateEto)
        {

        }

    }
}
