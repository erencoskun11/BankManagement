using Abp.BankManagement.Dtos.CardDtos;
using Abp.BankManagement.Entities;
using Abp.BankManagement.Localization;
using Abp.BankManagement.Managers;
using Abp.BankManagement.Models.Cards;
using Abp.BankManagement.Repositories;
using Abp.BankManagement.Etos.CardEtos;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace Abp.BankManagement.Services
{
    public class CardAppService : ApplicationService, ICardAppService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly CardManager _cardManager;
        private readonly IStringLocalizer<BankManagementResource> _localizer;
        private readonly IDistributedEventBus _distributedEventBus;

        public CardAppService(
            ICardRepository cardRepository,
            IObjectMapper objectMapper,
            CardManager cardManager,
            IStringLocalizer<BankManagementResource> localizer,
            IDistributedEventBus distributedEventBus)
        {
            _cardRepository = cardRepository;
            _objectMapper = objectMapper;
            _cardManager = cardManager;
            _localizer = localizer;
            _distributedEventBus = distributedEventBus;
        }

        public async Task<bool> BulkCreateAsync(List<CreateCardDto> cards)
        {
            foreach (var dto in cards)
            {
                var model = _objectMapper.Map<CreateCardDto, CardCreateModel>(dto);
                var card = _cardManager.Create(model);
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

                await _distributedEventBus.PublishAsync(eto);
            }

            return true;
        }

        public async Task<bool> CreateAsync(CreateCardDto input)
        {
            var model = _objectMapper.Map<CreateCardDto, CardCreateModel>(input);
            var card = _cardManager.Create(model);
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

            await _distributedEventBus.PublishAsync(eto);
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
            return _objectMapper.Map<Card, CardDto>(card);
        }

        public async Task<CardDto> GetByCardNumberAsync(string cardNumber)
        {
            var card = await _cardRepository.FindAsync(x => x.CardNumber == cardNumber);
            return _objectMapper.Map<Card, CardDto>(card);
        }

        public async Task<List<CardDto>> GetCardsByTypeAsync(Guid cardTypeId)
        {
            var cards = await _cardRepository.GetCardsByTypeAsync(cardTypeId) ?? new List<Card>();
            return _objectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<List<CardDto>> GetExpiredCardsAsync()
        {
            var cards = await _cardRepository.GetExpiredCardsAsync() ?? new List<Card>();
            return _objectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<List<CardDto>> GetInActiveCardAsync()
        {
            var cards = await _cardRepository.GetInActiveCardAsync() ?? new List<Card>();
            return _objectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<List<CardDto>> GetListAsync()
        {
            var cards = await _cardRepository.GetListAsync() ?? new List<Card>();
            return _objectMapper.Map<List<Card>, List<CardDto>>(cards);
        }

        public async Task<bool> UpdateAsync(UpdateCardDto input)
        {
            var card = await _cardRepository.GetAsync(input.Id);
            var model = _objectMapper.Map<UpdateCardDto, CardUpdateModel>(input);

            _cardManager.Update(card, model);
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

            await _distributedEventBus.PublishAsync(eto);
            return true;
        }
    }
}
