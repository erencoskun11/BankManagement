using System;

namespace Abp.BankManagement.Dtos.TransactionDtos
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid AccountId { get; set; }

        public Guid? CardId { get; set; }

        public Guid TransactionTypeId { get; set; }
    }
}
