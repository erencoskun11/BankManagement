using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Abp.BankManagement.Etos.TransactionDtos
{
    public class TransactionUpdateEto:EtoBase
    {
        public Guid TransactionId { get; set; }  // Güncellenecek Transaction ID'si

        public Guid? TenantId { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime? TransactionDate { get; set; }

        public Guid AccountId { get; set; }

        public Guid? CardId { get; set; }

        public Guid TransactionTypeId { get; set; }
    }
}
