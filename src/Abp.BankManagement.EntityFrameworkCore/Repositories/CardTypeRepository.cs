using System;
using Abp.BankManagement.Entities;
using Abp.BankManagement.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.BankManagement.Repositories
{
    public class CardTypeRepository : EfCoreRepository<BankManagementDbContext, CardType, Guid>, ICardTypeRepository
    {
        public CardTypeRepository(IDbContextProvider<BankManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


    }
}
