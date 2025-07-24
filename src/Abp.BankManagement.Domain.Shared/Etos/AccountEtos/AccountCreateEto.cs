using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace Abp.BankManagement.Etos.AccountEtos
{
    [EventName("AccountCreated")]
    public class AccountCreatedEto : EtoBase
    {
        public string AccountName { get; set; }
        public string IBAN { get; set; }
        public Guid CustomerId { get; set; }
    }
}
