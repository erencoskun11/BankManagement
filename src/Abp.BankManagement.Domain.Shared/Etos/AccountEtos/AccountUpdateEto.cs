using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace Abp.BankManagement.Etos.AccountEtos
{
    [EventName("AccountUpdated")]
    public class AccountUpdatedEto : EtoBase
    {
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpenedAt { get; set; }
    }
}
