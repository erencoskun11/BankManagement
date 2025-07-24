using System;

namespace Abp.BankManagement.Dtos.AccountDtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string IBAN { get; set; }

        public DateTime OpenedAt { get; set; }

        public bool IsActive { get; set; }

        public Guid CustomerId { get; set; }

        public string? CustomerName { get; set; } // İstersen includ
    }
}
