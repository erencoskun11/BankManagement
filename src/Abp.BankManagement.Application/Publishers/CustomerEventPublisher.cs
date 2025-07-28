using Abp.BankManagement.Etos.CustomerEtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Publishers
{
    public class CustomerEventPublisher : ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;


        public CustomerEventPublisher(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }
        public async Task PublishCustomerCreatedAsync(CustomerCreateEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

        public async Task PublishCustomerListedAsync(CustomerEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }

        public async Task PublishCustomerUpdatedAsync(CustomerUpdateEto eto)
        {
            await _distributedEventBus.PublishAsync(eto);
        }
    }
}
