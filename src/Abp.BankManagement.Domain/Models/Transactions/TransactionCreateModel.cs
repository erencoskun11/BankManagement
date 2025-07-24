using System;

namespace Abp.BankManagement.Models.Transactions
{
    public class TransactionCreateModel
    {
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public Guid? CardId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime? TransactionDate { get; set; }  // Opsiyonel, default UtcNow
    }
}
