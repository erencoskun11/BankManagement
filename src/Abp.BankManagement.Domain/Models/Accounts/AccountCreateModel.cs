using System;

namespace Abp.BankManagement.Models.Accounts
{
    public class AccountCreateModel
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public DateTime? OpenedAt { get; set; } // opsiyonel, default UTC now
        public bool IsActive { get; set; } = true;
        public Guid CustomerId { get; set; }
    }
}
