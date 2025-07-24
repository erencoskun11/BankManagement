using Abp.BankManagement.Dtos.CustomerDtos;
using Abp.BankManagement.Entities;
using Abp.BankManagement.Localization;
using Abp.BankManagement.Managers;
using Abp.BankManagement.Models.Customers;
using Abp.BankManagement.Repositories;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;
using Abp.BankManagement.ExceptionCodes;

namespace Abp.BankManagement.Services
{
    [UnitOfWork]
    public class CustomerAppService
        : ApplicationService,
          ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerManager _customerManager;
        private readonly IStringLocalizer<BankManagementResource> _stringLocalizer;

        public CustomerAppService(
            ICustomerRepository customerRepository,
            CustomerManager customerManager,
            IStringLocalizer<BankManagementResource> stringLocalizer)
        {
            _customerRepository = customerRepository;
            _customerManager = customerManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<bool> CreateAsync(CreateCustomerDto input)
        {
            var customerCreateModel = ObjectMapper.Map<CreateCustomerDto,CustomerCreateModel>(input);
            var customer = _customerManager.Create(customerCreateModel);
            await _customerRepository.InsertAsync(customer);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
            return true;
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<List<CustomerDto>> GetCustomersBornBeforeAsync(DateTime date)
        {
            var list = await _customerRepository.GetCustomersBornBeforeAsync(date);
            return ObjectMapper.Map<List<Customer>, List<CustomerDto>>(list);
        }

        public async Task<List<CustomerDto>> GetCustomersByRiskLimitAsync(decimal minRiskLimit)
        {
            var list = await _customerRepository.GetCustomersByRiskLimitAsync(minRiskLimit);
            return ObjectMapper.Map<List<Customer>, List<CustomerDto>>(list);
        }

        public async Task<List<CustomerDto>> GetListAsync()
        {
            var customers = await _customerRepository.GetListAsync();
            var dtos = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers);

            // Her DTO için hesap sayısını ekle
            foreach (var dto in dtos)
            {
                dto.AccountCount = await _customerRepository.GetAccountCountAsync(dto.Id);
            }

            return dtos;
        }

        public async Task<CustomerDto> GetNationalIdAsync(string nationalId)
        {
            var customer = await _customerRepository.GetByNationalIdAsync(nationalId);
            if (customer == null)
            {
                throw new UserFriendlyException($"Customer with NationalId '{nationalId}' not found.");
            }
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCustomerDto input)
        {
            var customer = await _customerRepository.GetAsync(id);
            if(customer == null)
            {
                throw new UserFriendlyException(_stringLocalizer[CustomerExceptionCodes.NotFoundException]);
            }
            var updateModel = ObjectMapper.Map<UpdateCustomerDto,CustomerUpdateModel>(input);
            var updatedCustomer = _customerManager.Update(customer, updateModel);
            await _customerRepository.UpdateAsync(customer);
            return true;
        }
    }
}
