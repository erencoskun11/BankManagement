using System;
using Abp.BankManagement.Entities;
using Abp.BankManagement.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.BankManagement.Repositories
{
    public class TransactionTypeRepository : EfCoreRepository<BankManagementDbContext, TransactionType, Guid>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(IDbContextProvider<BankManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
