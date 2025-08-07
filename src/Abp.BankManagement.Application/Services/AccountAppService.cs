using Abp.BankManagement.Dtos.AccountDtos;
using Abp.BankManagement.Entities;
using Abp.BankManagement.ExceptionCodes;
using Abp.BankManagement.Managers;
using Abp.BankManagement.Models.Accounts;
using Abp.BankManagement.Repositories;
using Abp.BankManagement.Etos.AccountEtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace Abp.BankManagement.Services
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AccountManager _accountManager;
        private readonly IDistributedEventBus _distributedEventBus;

        public AccountAppService(
            IAccountRepository accountRepository,
            AccountManager accountManager,
            IDistributedEventBus distributedEventBus)
        {
            _accountRepository = accountRepository;
            _accountManager = accountManager;
            _distributedEventBus = distributedEventBus;
        }

        public async Task<bool> BulkCreateAsync(List<CreateAccountDto> accounts)
        {
            foreach (var dto in accounts)
            {
                var model = ObjectMapper.Map<CreateAccountDto, AccountCreateModel>(dto);
                var account = _accountManager.Create(model);
                await _accountRepository.InsertAsync(account);

                var eto = new AccountCreatedEto
                {
                    AccountName = account.AccountName,
                    IBAN = account.IBAN,
                    CustomerId = account.CustomerId
                };

                await _distributedEventBus.PublishAsync(eto);
            }

            return true;
        }

        public async Task<bool> CreateAsync(CreateAccountDto input)
        {
            try
            {
                var model = ObjectMapper.Map<CreateAccountDto, AccountCreateModel>(input);
                var account = _accountManager.Create(model);
                await _accountRepository.InsertAsync(account);

               var eto = new AccountCreatedEto
                {
                    AccountName = account.AccountName,
                    IBAN = account.IBAN,
                    CustomerId = account.CustomerId
                };

                await _distributedEventBus.PublishAsync(eto);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Account creation failed!");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _accountRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<AccountDto>> GetAccountByCustomerId(Guid customerId)
        {
            var accounts = await _accountRepository.GetListAsync(x => x.CustomerId == customerId);
            if (accounts == null || accounts.Count == 0)
            {
                throw new UserFriendlyException($"No accounts found for customerId '{customerId}'.");
            }
            return ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
        }

        public async Task<AccountDto> GetAsync(Guid id)
        {
            var account = await _accountRepository.GetAsync(id);
            return ObjectMapper.Map<Account, AccountDto>(account);
        }

        public async Task<List<AccountDto>> GetLast10CreatedAccountAsync()
        {
            var accounts = await _accountRepository.GetLast10CreatedAccountAsync();
            return ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
        }

        public async Task<List<AccountDto>> GetListAsync()
        {
            var accounts = await _accountRepository.GetListAsync();
            return ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
        }

        public async Task<bool> UpdateAsync(UpdateAccountDto input)
        {
            try
            {
                var account = await _accountRepository.GetAsync(input.Id);
                if (account == null)
                {
                    throw new UserFriendlyException(AccountExceptionCodes.NotFoundException);
                }

                var model = ObjectMapper.Map<UpdateAccountDto, AccountUpdateModel>(input);
                _accountManager.Update(account, model);
                await _accountRepository.UpdateAsync(account);

                var eto = new AccountUpdatedEto
                {
                    AccountName = account.AccountName,
                    IsActive = account.IsActive,
                    OpenedAt = account.OpenedAt
                };

                await _distributedEventBus.PublishAsync(eto);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Account update failed for Id: {input.Id}");
                throw;
            }
        }
    }
}
