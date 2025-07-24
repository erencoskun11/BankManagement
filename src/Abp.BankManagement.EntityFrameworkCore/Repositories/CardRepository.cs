using Abp.BankManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;

namespace Abp.BankManagement.Repositories
{
    public class CardRepository : EfCoreRepository<BankManagementDbContext, Card, Guid>, ICardRepository

    {
        public CardRepository(Volo.Abp.EntityFrameworkCore.IDbContextProvider<BankManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Card> GetByCardNumberAsync(string cardNumber)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

       

        public async Task<List<Card>> GetCardsByTypeAsync(Guid cardTypeId, Card c)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(c => c.CardTypeId == cardTypeId)
                .ToListAsync();
        }

        public async Task<List<Card>> GetCardsByTypeAsync(Guid cardTypeId)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
            .Where(c=>c.CardTypeId == cardTypeId)
            .ToListAsync();

            
        }

        public async Task<List<Card>> GetExpiredCardsAsync()
        {
            var dbSet = await GetDbSetAsync();
            var now = DateTime.UtcNow;
            return await dbSet
                .Where(c => new DateTime(c.ExpiryYear , c.ExpiryMonth,1).AddMonths(1).AddDays(-1)<now)
                .ToListAsync();
        }

        public async Task<List<Card>> GetInActiveCardAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(c => !c.IsActive)
                .ToListAsync();
        }
    }
}
