using Abp.BankManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface IAccountTypeAppService
    {
        Task<AccountTypeDto> GetAsync(Guid id);
        Task<List<AccountTypeDto>> GetListAsync();
        Task<bool> CreateAsync(AccountTypeDto accountType);
        Task<bool> UpdateAsync(AccountTypeDto accountType);
        Task<bool>DeleteAsync(Guid id);
    }
}
