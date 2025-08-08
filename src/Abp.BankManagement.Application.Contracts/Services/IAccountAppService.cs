using Abp.BankManagement.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface IAccountAppService 
    {
        Task<AccountDto>GetAsync(Guid id);
        Task<List<AccountDto>> GetListAsync();
        Task<bool>CreateAsync(CreateAccountDto input);

        Task<bool>BulkCreateAsync(List<CreateAccountDto> accounts);
        Task<bool> UpdateAsync(UpdateAccountDto input);

        Task<bool> DeleteAsync(Guid id);

        Task<List<AccountDto>>GetAccountByCustomerId(Guid customerId);
        Task<List<AccountDto>> GetLast10CreatedAccountAsync();






    }
}
