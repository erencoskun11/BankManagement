using Abp.BankManagement.Entities;
using Abp.BankManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.BankManagement.Repositories
{
    public class CustomerRepository
        : EfCoreRepository<BankManagementDbContext, Customer, Guid>,
          ICustomerRepository
    {
        public CustomerRepository(IDbContextProvider<BankManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Customer> GetByNationalIdAsync(string nationalId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(c => c.NationalId == nationalId);
        }

        public async Task<List<Customer>> GetCustomersBornBeforeAsync(DateTime date)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(c => c.BirthDate < date).ToListAsync();
        }

        public async Task<List<Customer>> GetCustomersByRiskLimitAsync(decimal minRiskLimit)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(c => c.RiskLimit >= minRiskLimit).ToListAsync();
        }

        public async Task<int> GetAccountCountAsync(Guid customerId)
        {
            // UnitOfWork-aware olarak DbContext’e erişiyoruz
            var dbContext = await GetDbContextAsync();
            return await dbContext.Accounts.CountAsync(a => a.CustomerId == customerId);
        }
    }
}
