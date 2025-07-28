using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos;
using Abp.BankManagement.Dtos.CardTypeDtos;

namespace Abp.BankManagement.Services
{
    public interface ICardTypeAppService
    {
        Task<CardTypeDto>GetAsync(Guid id);
        Task<List<CardTypeDto>> GetListAsync();
        Task<bool> CreateAsync(CardTypeCreateDto cardType);
        Task<bool>UpdateAsync(CardTypeDto cardType);
        Task<bool> DeleteAsync(Guid id); 
    }
}
