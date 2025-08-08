using System;
using System.ComponentModel.DataAnnotations;

namespace Abp.BankManagement.Dtos.CardDtos
{
    public class UpdateCardDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(16)]
        public string CardNumber { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

        [Required, MaxLength(3)]
        public string CCV { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid CardTypeId { get; set; }
    }
}
