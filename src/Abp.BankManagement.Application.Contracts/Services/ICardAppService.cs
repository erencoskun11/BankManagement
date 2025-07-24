using Abp.BankManagement.Dtos.CardDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface ICardAppService  
    {

        Task<CardDto> GetAsync(Guid id);
        Task<List<CardDto>> GetListAsync();
        Task<bool> CreateAsync(CreateCardDto input);
        Task<bool> UpdateAsync(UpdateCardDto input);
        Task<bool> DeleteAsync(Guid id);


        Task<CardDto> GetByCardNumberAsync(string cardNumber);
        Task<List<CardDto>> GetExpiredCardsAsync();
        Task<List<CardDto>> GetCardsByTypeAsync(Guid cardTypeId);
        Task<List<CardDto>> GetInActiveCardAsync();


    }
}
