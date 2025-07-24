using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Repositories;

namespace Abp.BankManagement.Repositories
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<List<Account>> GetAccountByCustomerIdAsync(Guid customerId);
        Task<List<Account>> GetLast10CreatedAccountAsync();
        Task<Account> GetByIbanAsync(string iban,CancellationToken cancellationToken);
        Task<List<Account>> GetAccountsByTenantIdAsync(Guid tenantId);
    }
}

