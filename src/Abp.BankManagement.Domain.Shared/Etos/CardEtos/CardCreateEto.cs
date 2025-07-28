using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Abp.BankManagement.Etos.CardEtos
{
    [EventName("Card.Created")]
    public class CardCreateEto :EtoBase,IMultiTenant
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CCV { get; set; }
        public Guid AccountId { get; set; }
        public Guid CardTypeId { get; set; }
        public bool IsActive { get; set; } = true;

        public Guid? TenantId { get; set; }
    }
}
