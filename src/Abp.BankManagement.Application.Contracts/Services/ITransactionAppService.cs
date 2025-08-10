using Abp.BankManagement.Dtos.TransactionDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface ITransactionAppService
    {
        Task<TransactionDto> GetAsync(Guid id);
        Task<List<TransactionDto>> GetListAsync();
        Task<List<TransactionDto>> GetByAccountIdAsync(Guid accountId);
        Task<List<TransactionDto>> GetByCardIdAsync(Guid cardId);
        Task<List<TransactionDto>> GetByTypeIdAsync(Guid TransactionTypeId);
        Task<List<TransactionDto>> GetTransactionByDateRangeAsync(Guid accountId, DateTime start, DateTime end);
        Task<bool> CreateAsync(CreateTransactionDto input);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateTransactionDto input);
        Task<bool> BulkCreateAsync(IEnumerable<CreateTransactionDto>transactions);

    }
}
