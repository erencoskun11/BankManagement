using System;
using Abp.BankManagement.Dtos;
using Abp.BankManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Application.Services;

namespace Abp.BankManagement.Services
{
    public class CardTypeAppService:ApplicationService, ICardTypeAppService
    {
        private readonly ICardTypeRepository _cardTypeRepository; 

        public CardTypeAppService(ICardTypeRepository cardTypeRepository)
        {
            _cardTypeRepository = cardTypeRepository;
        }

        public async Task<bool> CreateAsync(CardTypeDto cardType)
        {
            var entity = ObjectMapper.Map<CardTypeDto, CardType>(cardType);
            await _cardTypeRepository.InsertAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _cardTypeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<CardTypeDto> GetAsync(Guid id)
        {
            var entity = await _cardTypeRepository.GetAsync(id);
            return ObjectMapper.Map<CardType, CardTypeDto>(entity);

        }

      public async Task<List<CardTypeDto>> GetListAsync()
{
    var entity = await _cardTypeRepository.GetListAsync();
    return ObjectMapper.Map<List<CardType>, List<CardTypeDto>>(entity);
}


        public async Task<bool> UpdateAsync(CardTypeDto cardType)
        {
            // fetch the existing entity
            var entity = await _cardTypeRepository.FindAsync(x=>x.Id==cardType.Id);

            // map the updated values from DTO to entity
            ObjectMapper.Map(cardType, entity);

            // save the updated entity
            await _cardTypeRepository.UpdateAsync(entity);

            return true;
        }

    }
}
