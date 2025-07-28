using Abp.BankManagement.Dtos;
using Abp.BankManagement.Dtos.CardTypeDtos;
using Abp.BankManagement.Repositories;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.BankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardTypeController : BankManagementController
    {
        private readonly ICardTypeAppService _cardTypeAppService;

        public CardTypeController(ICardTypeAppService cardTypeAppService)
        {
            _cardTypeAppService = cardTypeAppService;
        }

        [HttpGet]
        public async Task<List<CardTypeDto>> GetListAsync()
        {
            return await _cardTypeAppService.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<CardTypeDto> GetAsync(Guid id)
        {
            return await _cardTypeAppService.GetAsync(id)
;       }

        [HttpPost]
        public async Task<bool> CreateAsync(CardTypeCreateDto cardType)
        {
            return await _cardTypeAppService.CreateAsync(cardType);
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateAsync(Guid id, [FromBody] CardTypeDto cardType)
        {
            cardType.Id = id;
            return await _cardTypeAppService.UpdateAsync(cardType);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _cardTypeAppService.DeleteAsync(id);
        }

















    }
}
