using Abp.BankManagement.Caching;
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
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
//middleware ile generic api response oluşturulacak. Domain Shared içerisine. 
namespace Abp.BankManagement.Services
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AccountManager _accountManager;
        private readonly IDistributedEventBus _distributedEventBus;
       

        private static readonly DistributedCacheEntryOptions _defaultCacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };

        public AccountAppService(
            IAccountRepository accountRepository,
            AccountManager accountManager,
            IDistributedEventBus distributedEventBus,
            IDistributedCache<AccountDto> accountCache,
            IDistributedCache<List<AccountDto>> accountsCache)
        {
            _accountRepository = accountRepository;
            _accountManager = accountManager;
            _distributedEventBus = distributedEventBus;
         
        }

        private async Task PublishCreatedEtoAsync(Account account)
        {
            var eto = new AccountCreatedEto
            {
                AccountName = account.AccountName,
                IBAN = account.IBAN,
                CustomerId = account.CustomerId
            };

            await _distributedEventBus.PublishAsync(eto);
        }

        private AccountDto MapToDto(Account account) => ObjectMapper.Map<Account, AccountDto>(account);

        private async Task InvalidateCachesAfterWriteAsync(IEnumerable<Guid> accountIds)
        {
          
                return;
            }

        
        public async Task<bool> CreateAsync(CreateAccountDto input)
        {
            try
            {
                var model = ObjectMapper.Map<CreateAccountDto, AccountCreateModel>(input);
                var account = _accountManager.Create(model);
                await _accountRepository.InsertAsync(account);

                await PublishCreatedEtoAsync(account);
                await InvalidateCachesAfterWriteAsync(new[] { account.Id });

                Logger.LogInformation("Account created (Id: {AccountId}, IBAN: {Iban})", account.Id, account.IBAN);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to create account.");
                throw;
            }
        }

        public async Task<bool> BulkCreateAsync(List<CreateAccountDto> accounts)
        {
            var model = ObjectMapper.Map<List<CreateAccountDto>, List<Account>>(accounts);
            await _accountRepository.CreateListAsync(model);
            return true;
        }

        public async Task<AccountDto> GetAsync(Guid id)
        {
            var key = AccountCacheKeys.ItemKey(id);

         

            var account = await _accountRepository.GetAsync(id)
                ?? throw new UserFriendlyException(AccountExceptionCodes.NotFoundException);

            var dto = MapToDto(account);
            await Task.Delay(10000);

            return dto;
        }

        public async Task<List<AccountDto>> GetListAsync()
        {

            
            

            var accounts = await _accountRepository.GetListAsync();
            var mapped = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);

            return mapped;
        }


        public async Task<List<AccountDto>> GetLast10CreatedAccountAsync()
        {
            var key = AccountCacheKeys.Last10Key;
          
            var accounts = await _accountRepository.GetLast10CreatedAccountAsync();
            var mapped = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
            await Task.Delay(10000);

            return mapped;
        }

        public async Task<List<AccountDto>> GetAccountByCustomerId(Guid customerId)
        {
            var key = AccountCacheKeys.ListByCustomer(customerId);
          

            var accounts = await _accountRepository.GetListAsync(x => x.CustomerId == customerId);
            if (accounts == null || accounts.Count == 0)
            {
                throw new UserFriendlyException($"No account found for customerId '{customerId}'.");
            }

            var mapped = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);

            return mapped;





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

                var previousCustomerId = account.CustomerId;
                var model = ObjectMapper.Map<UpdateAccountDto, AccountUpdateModel>(input);
                _accountManager.Update(account, model);
                await _accountRepository.UpdateAsync(account);

                await _distributedEventBus.PublishAsync(new AccountUpdatedEto
                {
                    AccountName = account.AccountName,
                    IsActive = account.IsActive,
                    OpenedAt = account.OpenedAt
                });

                await InvalidateCachesAfterWriteAsync(new[] { account.Id });

              

                Logger.LogInformation("Account updated (Id: {AccountId})", account.Id);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Account update failed for Id: {AccountId}", input.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Account? account = null;
            try
            {
                account = await _accountRepository.GetAsync(id);
            }
            catch
            {
            }

            await _accountRepository.DeleteAsync(id);

            await InvalidateCachesAfterWriteAsync(new[] { id });

          

            Logger.LogInformation("Account deleted (Id: {AccountId})", id);
            return true;
        }
    }
}
