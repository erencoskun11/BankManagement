using Abp.BankManagement.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BankManagement.Services
{
    public interface ICustomerAppService 

    {
        Task<CustomerDto> GetAsync(Guid id);
        Task<List<CustomerDto>> GetListAsync();
        Task<bool> CreateAsync(CreateCustomerDto input);
        Task<bool> UpdateAsync(Guid id, UpdateCustomerDto input);
        Task<bool> DeleteAsync(Guid id);


        Task<CustomerDto> GetNationalIdAsync(string nationalId);
        Task<List<CustomerDto>> GetCustomersBornBeforeAsync(DateTime date);
        Task<List<CustomerDto>> GetCustomersByRiskLimitAsync(decimal minRiskLimit);

    }
}
