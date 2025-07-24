using System;

namespace Abp.BankManagement.Models.Cards
{
    public class CardCreateModel
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CCV { get; set; }
        public Guid AccountId { get; set; }
        public Guid CardTypeId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
