using Abp.BankManagement.Dtos.AccountTyoeDtos;
using Abp.BankManagement.Dtos.AccountTypeDtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : BankManagementController
    {
        private readonly IAccountTypeAppService _service;

        public AccountTypeController(IAccountTypeAppService service)
        {
            _service = service;
        }

        // GET: api/AccountType
        [HttpGet]
        public Task<List<AccountTypeDto>> GetAll()
        {
            return _service.GetListAsync();
        }

        // GET: api/AccountType/{id}
        [HttpGet("{id}")]
        public Task<AccountTypeDto> Get(Guid id)
        {
            return _service.GetAsync(id);
        }

        // POST: api/AccountType
        [HttpPost]
        public Task<bool> Create([FromBody] AccountTypeCreateDto input)
        {
            return _service.CreateAsync(input);
        }

        // PUT: api/AccountType
        [HttpPut]
        public Task<bool> Update([FromBody] AccountTypeDto input)
        {
            return _service.UpdateAsync(input);
        }

        // DELETE: api/AccountType/{id}
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}