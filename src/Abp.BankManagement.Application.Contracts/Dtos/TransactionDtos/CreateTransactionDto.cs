using System;

namespace Abp.BankManagement.Dtos.TransactionDtos
{

    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime? TransactionDate { get; set; }

        public Guid AccountId { get; set; }

        public Guid? CardId { get; set; }

        public int TransactionTypeId { get; set; }
    }
}
