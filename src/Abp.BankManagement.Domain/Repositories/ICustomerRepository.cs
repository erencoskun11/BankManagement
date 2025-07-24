using Abp.BankManagement.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.BankManagement.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<Customer> GetByNationalIdAsync(string nationalId);
        Task<List<Customer>> GetCustomersByRiskLimitAsync(decimal minRiskLimit);
        Task<List<Customer>> GetCustomersBornBeforeAsync(DateTime date);
        Task<int> GetAccountCountAsync(Guid customerId);


    }
}
