using Abp.BankManagement.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Abp.BankManagement.Entities;

namespace Abp.BankManagement.Repositories
{
    public class AccountRepository : EfCoreRepository<BankManagementDbContext, Account, Guid>, IAccountRepository
    {
        public AccountRepository(IDbContextProvider<BankManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Account>> GetAccountByCustomerIdAsync(Guid customerId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
        }
        public async Task<List<Account>>GetLast10CreatedAccountAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .OrderByDescending(a=>a.CreationTime)
                .Take(10)
                .ToListAsync();
        }
        public async Task<Account> GetByIbanAsync(string iban, CancellationToken cancellationToken)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .FirstOrDefaultAsync(a => a.IBAN == iban, cancellationToken: cancellationToken);
        }

        public async Task<List<Account>> GetAccountsByTenantIdAsync(Guid tenantId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(a => a.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task CreateListAsync(List<Account> accounts, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            await dbSet.AddRangeAsync(accounts, cancellationToken);
        }
    }
}

