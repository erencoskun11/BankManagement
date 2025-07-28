using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace Abp.BankManagement.Etos.CardEtos
{
    [EventName("Card.Updated")]
    public class CardUpdateEto : EtoBase
    {
        public Guid CardId { get; set; } 

        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CCV { get; set; }
        public Guid AccountId { get; set; }
        public Guid CardTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
