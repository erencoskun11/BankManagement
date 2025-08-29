using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Attributes;
using Abp.BankManagement.Caching;
using Abp.BankManagement.Dtos.AccountDtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BankManagementController
    {
        private readonly IAccountAppService _service;

        public AccountController(IAccountAppService service)
        {
            _service = service;
        }

        
        [HttpPost]
        [CacheRefresh(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.ListKey), nameof(CacheKeys<AccountDto>.Last10Key))]
        public async Task<bool> Create([FromBody] CreateAccountDto dto)
        {
            return await _service.CreateAsync(dto);
        }

        
        [HttpPost("bulk-create")]
        [CacheRefresh(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.ListKey), nameof(CacheKeys<AccountDto>.Last10Key))]
        public async Task<bool> BulkCreateAsync([FromBody] List<CreateAccountDto> accounts)
        {
            return await _service.BulkCreateAsync(accounts);
        }

        [HttpDelete("{id}")]
        [CacheRefresh(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.ListKey), nameof(CacheKeys<AccountDto>.Last10Key))]
        public async Task<bool> Delete(Guid id)
        {
            return await _service.DeleteAsync(id);
        }

        
        [HttpGet("{id}")]
        [Cacheable(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.ItemKeyTemplate), seconds: 600)]
        public async Task<AccountDto> Get(Guid id)
        {
            return await _service.GetAsync(id);
        }

        
        [HttpGet]
        [Cacheable(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.ListKey), seconds: 600)]
        public async Task<List<AccountDto>> GetAll()
        {
            return await _service.GetListAsync();
        }

        
        [HttpPut]
        [CacheRefresh(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.ListKey), nameof(CacheKeys<AccountDto>.Last10Key))]
        public async Task<bool> Update([FromBody] UpdateAccountDto dto)
        {
            return await _service.UpdateAsync(dto);
        }

        
        [HttpGet("by-customer/{customerId}")]
        public async Task<List<AccountDto>> GetByCustomer(Guid customerId)
        {
            return await _service.GetAccountByCustomerId(customerId);
        }

        
        [HttpGet("last10")]
        [Cacheable(typeof(CacheKeys<AccountDto>), nameof(CacheKeys<AccountDto>.Last10Key), seconds: 600)]
        public async Task<List<AccountDto>> GetLast10()
        {
            return await _service.GetLast10CreatedAccountAsync();
        }
    }
}
