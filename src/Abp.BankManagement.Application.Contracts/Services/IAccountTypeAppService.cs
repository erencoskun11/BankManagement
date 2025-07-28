using Abp.BankManagement.Dtos.AccountTyoeDtos;
using Abp.BankManagement.Dtos.AccountTypeDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface IAccountTypeAppService
    {
        Task<AccountTypeDto> GetAsync(Guid id);
        Task<List<AccountTypeDto>> GetListAsync();
        Task<bool> CreateAsync(AccountTypeCreateDto accountType);
        Task<bool> UpdateAsync(AccountTypeDto accountType);
        Task<bool>DeleteAsync(Guid id);
    }
}
