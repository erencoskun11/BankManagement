using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos.CustomerDtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]

    public class CustomerController : BankManagementController
    {
        private readonly ICustomerAppService _service;

        public CustomerController(ICustomerAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerDto input)
        {
            if (!ModelState.IsValid)
            {
                // ModelState’deki tüm hataları JSON olarak döndür
                return BadRequest(ModelState);
            }

            await _service.CreateAsync(input);
            return Ok(true);
        }




        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _service.DeleteAsync(id);
        }

        [HttpGet("list")]
        public async Task<List<CustomerDto>> GetListAsync()
        {
            return await _service.GetListAsync();
        }

        [HttpGet("by-risk-limit/{minRiskLimit}")]
        public async Task<List<CustomerDto>> GetByRiskLimit(decimal minRiskLimit)
        {
            return await _service.GetCustomersByRiskLimitAsync(minRiskLimit);
        }

        [HttpGet("born-before")]
        public async Task<List<CustomerDto>> GetBornBefore([FromQuery] DateTime date)
        {
            return await _service.GetCustomersBornBeforeAsync(date);
        }

        [HttpGet("by-national-id/{nationalId}")]
        public async Task<CustomerDto> GetByNationalId(string nationalId)
        {
            return await _service.GetNationalIdAsync(nationalId);
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateAsync(Guid id, [FromBody] UpdateCustomerDto input)
        {
            return await _service.UpdateAsync(id, input);
        }
    }
}
