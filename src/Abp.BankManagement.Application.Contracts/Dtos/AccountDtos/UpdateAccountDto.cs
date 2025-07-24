using System;

namespace Abp.BankManagement.Dtos.AccountDtos
{
    public class UpdateAccountDto
    {
        public Guid Id { get; set; } // Güncellemede şart
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public bool IsActive { get; set; }
    }
}
