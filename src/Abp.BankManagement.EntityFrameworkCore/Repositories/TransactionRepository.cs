using Abp.BankManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.BankManagement.Repositories
{
    public class TransactionRepository : EfCoreRepository<BankManagementDbContext, Transaction, Guid>, ITransactionRepository
    {
        public TransactionRepository(IDbContextProvider<BankManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<Transaction>> GetByAccountIdAsync(Guid accountId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(t=>t.AccountId == accountId).ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByTypeIdAsync(Guid transactionTypeId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(t =>t.TransactionTypeId ==transactionTypeId).ToListAsync();
        }

        public async Task<List<Transaction>> GetByCardIdAsync(Guid cardId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(t => t.CardId.HasValue && t.CardId.Value == cardId)
                .ToListAsync();
        }
        public async Task<List<Transaction>> GetByTypeIdAsync(Guid transactionTypeId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(t => t.TransactionTypeId == transactionTypeId).ToListAsync();
        }


        public async Task<List<Transaction>> GetTransactionByDateRangeAsync(Guid accountId, DateTime start, DateTime end)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(t => t.AccountId == accountId &&
                            t.TransactionDate >= start &&
                            t.TransactionDate <= end)
                .ToListAsync();
        }


        
    }
}
