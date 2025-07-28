using Abp.BankManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.BankManagement.Entities;
using Volo.Abp.Application.Services;
using Abp.BankManagement.Dtos.AccountTyoeDtos;
using Abp.BankManagement.Dtos.AccountTypeDtos;

namespace Abp.BankManagement.Services
{
    public class AccountTypeAppService : ApplicationService, IAccountTypeAppService
    {
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountTypeAppService(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }
        public async Task<bool>CreateAsync(AccountTypeCreateDto accountType)
        {
            var entity = ObjectMapper.Map<AccountTypeCreateDto, AccountType>(accountType);
            await _accountTypeRepository.InsertAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _accountTypeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<AccountTypeDto> GetAsync(Guid id)
        {
            var entity = await _accountTypeRepository.GetAsync(id);
            return ObjectMapper.Map<AccountType,AccountTypeDto>(entity);
        }

        public async Task<List<AccountTypeDto>> GetListAsync()
        {
            var entities = await _accountTypeRepository.GetListAsync();
            return ObjectMapper.Map<List<AccountType>,List<AccountTypeDto>>(entities);
        }

        public async Task<bool> UpdateAsync(AccountTypeDto accountType)
        {
            var entity = await _accountTypeRepository.GetAsync(accountType.Id);
            ObjectMapper.Map(accountType, entity);
            await _accountTypeRepository.UpdateAsync(entity);
            return true;
        }
    }
}
