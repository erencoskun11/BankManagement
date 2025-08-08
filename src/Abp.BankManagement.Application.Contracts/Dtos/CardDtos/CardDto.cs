using System;

namespace Abp.BankManagement.Dtos.CardDtos
{
    public class CardDto
    {
        public Guid Id { get; set; }

        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string CCV { get; set; }

        public bool IsActive { get; set; }

        public Guid AccountId { get; set; }

        public string? AccountName { get; set; } // Opsiyonel

        public Guid CardTypeId { get; set; }

        public string? CardTypeName { get; set; } // Opsiyonel

    }
}
