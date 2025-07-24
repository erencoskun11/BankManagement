using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos.TransactionDtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BankManagementController
    {
        private readonly ITransactionAppService _transactionAppService;

        public TransactionController(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }

        [HttpPost]
        public async Task<bool> Create([FromBody] CreateTransactionDto input)
        {
            return await _transactionAppService.CreateAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _transactionAppService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<TransactionDto> Get(Guid id)
        {
            return await _transactionAppService.GetAsync(id);
        }

        [HttpGet("byaccount/{accountId}")]
        public async Task<List<TransactionDto>> GetByAccountId(Guid accountId)
        {
            return await _transactionAppService.GetByAccountIdAsync(accountId);
        }

        [HttpGet("bycard/{cardId}")]
        public async Task<List<TransactionDto>> GetByCardId(Guid cardId)
        {
            return await _transactionAppService.GetByCardIdAsync(cardId);
        }

        [HttpGet("bydaterange/{accountId}")]
        public async Task<List<TransactionDto>> GetByDateRange(Guid accountId, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return await _transactionAppService.GetTransactionByDateRangeAsync(accountId, start, end);
        }

        [HttpGet("bytype/{transactionTypeId}")]
        public async Task<List<TransactionDto>> GetByType(Guid transactionTypeId)
        {
            return await _transactionAppService.GetByTypeIdAsync(transactionTypeId);
        }

        [HttpGet]
        public async Task<List<TransactionDto>> GetAll()
        {
            return await _transactionAppService.GetListAsync();
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(Guid id, [FromBody] UpdateTransactionDto input)
        {
            return await _transactionAppService.UpdateAsync(id, input);
        }
    }
}
