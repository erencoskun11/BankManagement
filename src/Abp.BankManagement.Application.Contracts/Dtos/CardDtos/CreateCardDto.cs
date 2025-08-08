using System;
using System.ComponentModel.DataAnnotations;

namespace Abp.BankManagement.Dtos.CardDtos
{
    public class CreateCardDto
    {
        
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CCV { get; set; }
        public bool IsActive { get; set; } = true;  
        public Guid AccountId { get; set; }
        public Guid CardTypeId { get; set; }
    }
}
