using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// Hesap oluşturmak için kullanılan api.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Create([FromBody]CreateAccountDto dto)
        {
            return await _service.CreateAsync(dto);
        }
        [HttpDelete("{id}")]
        public async Task<bool>Delete(Guid id)
        {
            return await _service.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<AccountDto>Get(Guid id)
        {
            return await _service.GetAsync(id);
        }
        [HttpGet]
        public async Task<List<AccountDto>> GetAll()
        {
            return await _service.GetListAsync();
        }

        [HttpPut]
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
        public async Task<List<AccountDto>> GetLast10()
        {
            return await _service.GetLast10CreatedAccountAsync();
        }



    }
}
/*
 | CustomerId                             | FullName     | NationalId  |
 | -------------------------------------- | ------------ | ----------- |
 | 1D8517F0-9C5B-429C-85FB-028D2202ACF4   | Ahmet Yılmaz | 12345678901 |
 | 7F2533E0-4F89-11D3-9A0C-0305A82C3301   | Ahmet Yılmaz | 12345678901 |
 | 7F2504E0-4F89-11D3-9A0C-0305E82C3301   | Ahmet Yılmaz | 12345678901 |
 | 7F2533E0-4F89-11D3-9A0C-0305E82C3301   | Ahmet Yılmaz | 12345678901 |
 | 11111111-1111-1111-1111-111111111111   | Eren Yılmaz  | 12345678901 |
 | 11111111-1111-1331-1111-111111111111   | Eren Yılmaz  | 12345678901 |
 | 32C49FAA-E182-48B9-AC8B-3595562335C3   | Ayşe Demir   | 94465431109 |
 | F391EE6A-6FF6-4D75-A8C7-633F765F065E   | Eren Yılmaz  | 12345678901 |
 | 77E17698-890A-439E-BADA-7758CBC26D95   | Ahmet Yılmaz | 12345678901 |
 | F68ABEA6-5A77-4DD1-AE20-B97E9A2EC1FE   | Ayşe Demir   | 98765432109 |
 | 1E227C89-91F7-47D6-A9D3-C5213609AC44   | Ayşe Demir   | 94465431109 |
 | E24B5F54-C7C0-403A-8CCD-D1EC281380FA   | Ahmet Yılmaz | 12345678901 |
 | AD7281C8-4896-401A-9EE9-D379082543A5   | Ayşe Demir   | 94465431109 |
*/
