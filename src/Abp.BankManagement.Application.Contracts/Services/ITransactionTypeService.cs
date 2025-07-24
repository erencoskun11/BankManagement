using System;
using Abp.BankManagement.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public  interface ITransactionTypeService
    {
        Task<TransactionTypeDto>GetAsync(Guid id);
        Task<List<TransactionTypeDto>> GetListAsync();
        Task<bool> CreateAsync(TransactionTypeDto transactionTypeDto);
        Task<bool> UpdateAsync(TransactionTypeDto transactionTypeDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
