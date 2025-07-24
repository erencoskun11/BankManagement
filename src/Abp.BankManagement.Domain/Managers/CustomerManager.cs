using Abp.BankManagement.Entities;
using Abp.BankManagement.Models.Customers;
using Volo.Abp.Domain.Services;

namespace Abp.BankManagement.Managers
{
    public class CustomerManager : DomainService
    {
        // Manager içinde create update için model oluşturabiliriz.Böylelikle servis içinde
        // kirli görüntüyü azaltmış olur.
        public Customer Create(CustomerCreateModel customerCreateModel)
        {
            return new Customer(
                CurrentTenant.Id,
                customerCreateModel.FullName,
                customerCreateModel.NationalId,
                customerCreateModel.BirthPlace,
                customerCreateModel.BirthDate,
                customerCreateModel.RiskLimit
            );
        }

        public Customer Update (Customer customer, CustomerUpdateModel customerUpdateModel)
        {
            customer.FullName = customerUpdateModel.FullName;
            customer.BirthPlace = customerUpdateModel.BirthPlace;
            customer.BirthDate = customerUpdateModel.BirthDate;
            customer.RiskLimit = customerUpdateModel.RiskLimit;

            return customer;
        }
    }
}