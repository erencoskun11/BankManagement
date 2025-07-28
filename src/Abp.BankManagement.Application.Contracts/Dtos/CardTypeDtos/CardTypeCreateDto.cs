using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.BankManagement.Dtos.CardTypeDtos
{
    public class CardTypeCreateDto
    {
        [Required]
//gecici olarak yazdım 
        public string Name { get; set; }
    }
}
