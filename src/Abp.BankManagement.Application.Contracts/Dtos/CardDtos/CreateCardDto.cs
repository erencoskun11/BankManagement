using System;
using System.ComponentModel.DataAnnotations;

namespace Abp.BankManagement.Dtos.CardDtos
{
    public class CreateCardDto
    {
        [Required, MaxLength(16)]
        public string CardNumber { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

        [Required, MaxLength(3)]
        public string CCV { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public int CardTypeId { get; set; }
    }
}
