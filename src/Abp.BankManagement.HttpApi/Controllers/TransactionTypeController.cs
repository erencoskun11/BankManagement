using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : BankManagementController
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        [HttpPost]
        public async Task<bool> Create([FromBody] TransactionTypeDto input)
        {
            return await _transactionTypeService.CreateAsync(input);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] TransactionTypeDto input)
        {
            return await _transactionTypeService.UpdateAsync(input);
        }
        [HttpGet("{id}")]
        public async Task<TransactionTypeDto> Get(Guid id)
        {
            return await _transactionTypeService.GetAsync(id);
        }
        [HttpGet]
        public async Task<List<TransactionTypeDto>> GetAll()
        {
            return await _transactionTypeService.GetListAsync();
        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _transactionTypeService.DeleteAsync(id);
        }
    }
}
