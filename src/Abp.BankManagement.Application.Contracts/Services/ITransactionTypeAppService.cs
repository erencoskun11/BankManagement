using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos.TransactionTypeDtos;

namespace Abp.BankManagement.Services
{
    public  interface ITransactionTypeAppService
    {
        Task<TransactionTypeDto>GetAsync(Guid id);
        Task<List<TransactionTypeDto>> GetListAsync();
        Task<bool> CreateAsync(TransactionTypeCreateDto transactionTypeDto);
        Task<bool> UpdateAsync(TransactionTypeDto transactionTypeDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
