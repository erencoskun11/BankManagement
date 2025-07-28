using Abp.BankManagement.Dtos.CardDtos;
using Abp.BankManagement.Localization;
using Abp.BankManagement.Managers;
using Abp.BankManagement.Repositories;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using Abp.BankManagement.Models.Cards;
using Abp.BankManagement.Publishers;
using Abp.BankManagement.Etos.CardEtos;
namespace Abp.BankManagement.Services
{
    public class CardAppService : ApplicationService, ICardAppService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IObjectMapper _ObjectMapper;
        private readonly CardManager _cardManager;
        private readonly IStringLocalizer<BankManagementResource> _localizer;
        private readonly CardEventPublisher _cardEventPublisher; 

        public CardAppService(ICardRepository cardRepository, 
            IObjectMapper ObjectMapper,
            CardManager cardManager,
            IStringLocalizer<BankManagementResource> localizer,
            CardEventPublisher cardEventPublisher)
        {
            _cardRepository = cardRepository;
            _ObjectMapper = ObjectMapper;
            _cardManager = cardManager;
            _localizer = localizer;
            _cardEventPublisher = cardEventPublisher;
        }

        public async Task<bool> CreateAsync(CreateCardDto input)
        {
            var createModel = ObjectMapper.Map<CreateCardDto, CardCreateModel>(input);
            var card = _cardManager.Create(createModel);

            await _cardRepository.InsertAsync(card);

            var eto = new CardCreateEto
            {
                CardNumber = card.CardNumber,
                ExpiryYear = card.ExpiryYear,
                ExpiryMonth = card.ExpiryMonth,
                CCV = card.CCV,
                AccountId = card.AccountId,
                CardTypeId = card.CardTypeId,
                IsActive = card.IsActive
            };
            await _cardEventPublisher.PublicCardCreateAsync(eto);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _cardRepository.DeleteAsync(id);
            return true;
        }

        public async Task<CardDto> GetAsync(Guid id)
        {
            var card = await _cardRepository.GetAsync(id);
            return ObjectMapper.Map<Card, CardDto>(card);
        }

        public async Task<CardDto> GetByCardNumberAsync(string cardNumber)
        {
            var card = await _cardRepository.FindAsync(x => x.CardNumber == cardNumber);
            return ObjectMapper.Map<Card, CardDto>(card); // Null dönebilir, exception yok
        }

        public async Task<List<CardDto>> GetCardsByTypeAsync(Guid cardTypeId)
        {
            var cards = await _cardRepository.GetCardsByTypeAsync(cardTypeId) ?? new List<Card>();
            return ObjectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<List<CardDto>> GetExpiredCardsAsync()
        {
            var cards = await _cardRepository.GetExpiredCardsAsync() ?? new List<Card>();
            return ObjectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<List<CardDto>> GetInActiveCardAsync()
        {
            var cards = await _cardRepository.GetInActiveCardAsync() ?? new List<Card>();
            return ObjectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<List<CardDto>> GetListAsync()
        {
            var cards = await _cardRepository.GetListAsync() ?? new List<Card>();
            return ObjectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<bool> UpdateAsync(UpdateCardDto input)
        {
            var card = await _cardRepository.GetAsync(input.Id);

            var updateModel = ObjectMapper.Map<UpdateCardDto, CardUpdateModel>(input);
            await _cardRepository.UpdateAsync(card);

            var eto = new CardUpdateEto
            {
                CardId = card.Id,
                CardNumber = card.CardNumber,
                ExpiryMonth = card.ExpiryMonth,
                ExpiryYear = card.ExpiryYear,
                CCV = card.CCV,
                AccountId = card.AccountId,
                CardTypeId = card.CardTypeId,
                IsActive = card.IsActive
            };
            await _cardEventPublisher.PublishCardUpdatedAsync(eto);


            return true;
        }
    }
}

