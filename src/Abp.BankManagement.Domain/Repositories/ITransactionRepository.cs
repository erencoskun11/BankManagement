using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Transaction = Abp.BankManagement.Entities.Transaction;

namespace Abp.BankManagement.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction,Guid>
    {
        Task<List<Transaction>> GetByAccountIdAsync(Guid accountId);
        Task<List<Transaction>> GetByCardIdAsync(Guid cardId);
        Task<List<Transaction>> GetTransactionByDateRangeAsync(Guid accountId, DateTime start, DateTime end);
        Task<List<Transaction>> GetTransactionsByTypeIdAsync(Guid transactionTypeId);
        Task<List<Transaction>> GetByTypeIdAsync(Guid transactionTypeId);
        

    }
}
