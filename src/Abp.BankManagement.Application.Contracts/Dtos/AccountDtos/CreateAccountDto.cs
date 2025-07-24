using System;
using System.ComponentModel.DataAnnotations;

namespace Abp.BankManagement.Dtos.AccountDtos
{
    public class CreateAccountDto
    {
        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(26)]
        public string IBAN { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public Guid CustomerId { get; set; }
    }
}
