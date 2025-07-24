using System;
using Abp.BankManagement.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface ICardTypeAppService
    {
        Task<CardTypeDto>GetAsync(Guid id);
        Task<List<CardTypeDto>> GetListAsync();
        Task<bool> CreateAsync(CardTypeDto cardType);
        Task<bool>UpdateAsync(CardTypeDto cardType);
        Task<bool> DeleteAsync(Guid id); 
    }
}
