/*using Abp.BankManagement.Etos.CardEtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.EventHandlers
{
    public class CardEventHandler :
        IDistributedEventHandler<CardCreateEto>,
        IDistributedEventHandler<CardUpdateEto>,
        IDistributedEventHandler<CardEto>
    {
        private readonly ILogger<CardEventHandler> _logger;

        public CardEventHandler(ILogger<CardEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleEventAsync(CardEto eventData)
        {
            _logger.LogInformation($"[Card Listed] Number: {eventData.CardNumber}");
            return Task.CompletedTask;
        }

        Task IDistributedEventHandler<CardCreateEto>.HandleEventAsync(CardCreateEto eventData)
        {
            _logger.LogInformation($"[Card Created] Number: {eventData.CardNumber}");
            return Task.CompletedTask;
        }

        Task IDistributedEventHandler<CardUpdateEto>.HandleEventAsync(CardUpdateEto eventData)
        {
            _logger.LogInformation($"[Card Updated] Number: {eventData.CardNumber}");
            return Task.CompletedTask;
        }
    }

    /*
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
    */