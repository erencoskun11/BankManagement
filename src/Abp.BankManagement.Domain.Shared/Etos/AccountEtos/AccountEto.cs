using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.EventBus;

namespace Abp.BankManagement.Etos.AccountEtos
{
    [EventName("Account.Listed")]
    public class AccountEto : EtoBase, IMultiTenant
    {
        public Guid? TenantId {  get; set; }
        public string AccountName{ get; set; }
        public string AccountNumber{ get; set; }
        public string IBAN {  get; set; }
        public DateTime OpenedAt { get; set; }
        public bool IsActive { get; set; }
        public Guid CustomerId { get; set; }
    }
}
