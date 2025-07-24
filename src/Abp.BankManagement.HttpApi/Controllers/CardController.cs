using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos.CardDtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CardController : BankManagementController
    {
        private readonly ICardAppService _cardAppService;

        public CardController(ICardAppService cardAppService)
        {
            _cardAppService = cardAppService;
        }
        [HttpPost]
        public async Task<bool> Create([FromBody] CreateCardDto input)
            {
            return await _cardAppService.CreateAsync(input);
            }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _cardAppService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<CardDto> Get(Guid id)
        {
            return await _cardAppService.GetAsync(id);
        }

        [HttpGet("bynumber/{cardNumber}")]
        public async Task<CardDto> GetByCardNumber(string cardNumber)
        {
            return await _cardAppService.GetByCardNumberAsync(cardNumber);
        }
        [HttpGet("bytype/{cardTypeId}")]

        public async Task<List<CardDto>> GetByType(Guid cardTypeId)
        {
            return await _cardAppService.GetCardsByTypeAsync(cardTypeId);
        }
        [HttpGet("expired")]
        public async Task<List<CardDto>> GetExpired()
        {
            return await _cardAppService.GetExpiredCardsAsync();
        }

        [HttpGet("inactive")]
        public async Task<List<CardDto>> GetInactive()
        {
            return await _cardAppService.GetInActiveCardAsync();
        }

        [HttpGet]
        public async Task<List<CardDto>> GetAll()
        {
            return await _cardAppService.GetListAsync();
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] UpdateCardDto input)
        {
            return await _cardAppService.UpdateAsync(input);
        }



    }
}
