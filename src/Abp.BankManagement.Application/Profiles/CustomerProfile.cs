using Abp.BankManagement.Dtos.CustomerDtos;
using Abp.BankManagement.Models.Customers;
using AutoMapper;

namespace Abp.BankManagement.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() {
            CreateMap<CreateCustomerDto, CustomerCreateModel>();
        
        }
    }
}
