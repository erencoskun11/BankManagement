using Abp.BankManagement.EntityFrameworkCore;
using System;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.BankManagement.Repositories
{
    public class AccountTypeRepository : EfCoreRepository<BankManagementDbContext, AccountType, Guid>, IAccountTypeRepository
    {
        public AccountTypeRepository(IDbContextProvider<BankManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
