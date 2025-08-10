using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Dtos.CustomerDtos;
using Abp.BankManagement.Etos.CustomerEtos;
using Abp.BankManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]

    public class CustomerController : BankManagementController
    {
        private readonly ICustomerAppService _service;
        private readonly IDistributedEventBus _distributedEventBus;

        public CustomerController(ICustomerAppService service, IDistributedEventBus distributedEventBus)
        {
            _service = service;
            _distributedEventBus = distributedEventBus;

        }

        [HttpPost("test-publish")]
        public async Task<bool> TestPublishCustomerCreatedEventAsync()
        {
            var eto = new CustomerCreateEto
            {
                CustomerId = Guid.NewGuid(),
                FullName = "Test User",
                NationalId = "12345678901",
                BirthPlace = "Test City",
                BirthDate = DateTime.UtcNow,
                RiskLimit = 1000
            };

            await _distributedEventBus.PublishAsync(eto);

            return true;
        }

        [HttpPost]
        public async Task<bool> CreateAsync([FromBody] CreateCustomerDto input)
        {
            return await _service.CreateAsync(input);   
        }

        [HttpPost("bulk-create")]
        public async Task<bool> BulkCreateAsync([FromBody] List<CreateCustomerDto> customers)
        {
            return await _service.BulkCreateAsync(customers);
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
