using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Repositories;

namespace Abp.BankManagement.Repositories
{
    public interface ICardRepository : IRepository<Card, Guid>
    {
        Task<Card> GetByCardNumberAsync(string cardNumber);
        Task<List<Card>> GetExpiredCardsAsync();
        Task<List<Card>>GetCardsByTypeAsync(Guid cardTypeId);
        Task<List<Card>> GetInActiveCardAsync();
    }
}
