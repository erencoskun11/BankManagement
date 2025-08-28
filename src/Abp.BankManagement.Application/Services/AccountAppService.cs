
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
        private readonly IDistributedCache<AccountDto> _accountCache;
        private readonly IDistributedCache<List<AccountDto>> _accountsCache;

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
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            _distributedEventBus = distributedEventBus ?? throw new ArgumentNullException(nameof(distributedEventBus));
            _accountCache = accountCache ?? throw new ArgumentNullException(nameof(accountCache));
            _accountsCache = accountsCache ?? throw new ArgumentNullException(nameof(accountsCache));
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
            var ids = (accountIds ?? Enumerable.Empty<Guid>()).ToList();
            if (ids.Count == 0)
            {
                await _accountsCache.RemoveAsync(AccountCacheKeys.ListKey);
                await _accountsCache.RemoveAsync(AccountCacheKeys.Last10Key);
                return;
            }

            foreach (var id in ids)
            {
                try
                {
                    await _accountCache.RemoveAsync(AccountCacheKeys.ItemKey(id));
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex, "Failed to remove item cache for accountId {AccountId}", id);
                }
            }

            await _accountsCache.RemoveAsync(AccountCacheKeys.ListKey);
            await _accountsCache.RemoveAsync(AccountCacheKeys.Last10Key);

            try
            {
                var accounts = await _accountRepository.GetListAsync(x => ids.Contains(x.Id));
                var customerIds = accounts.Select(a => a.CustomerId).Distinct();
                foreach (var customerId in customerIds)
                {
                    await _accountsCache.RemoveAsync(AccountCacheKeys.ListByCustomer(customerId));
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "Failed to fetch accounts while invalidating customer-specific caches.");
            }
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
            if (accounts == null || accounts.Count == 0)
            {
                Logger.LogWarning("BulkCreateAsync called with empty accounts list.");
                return true;
            }

            var createdIds = new List<Guid>(capacity: accounts.Count);

            foreach (var dto in accounts)
            {
                var model = ObjectMapper.Map<CreateAccountDto, AccountCreateModel>(dto);
                var account = _accountManager.Create(model);
                await _accountRepository.InsertAsync(account);

                createdIds.Add(account.Id);
                await PublishCreatedEtoAsync(account);
            }

            await InvalidateCachesAfterWriteAsync(createdIds);

            Logger.LogInformation("Bulk create finished. CreatedCount={Count}", createdIds.Count);
            return true;
        }

        public async Task<AccountDto> GetAsync(Guid id)
        {
            var key = AccountCacheKeys.ItemKey(id);

            var cached = await _accountCache.GetAsync(key);
            if (cached != null)
            {
                return cached;
            }

            var account = await _accountRepository.GetAsync(id)
                ?? throw new UserFriendlyException(AccountExceptionCodes.NotFoundException);

            var dto = MapToDto(account);
            await Task.Delay(10000);

            await _accountCache.SetAsync(key, dto, _defaultCacheOptions);
            return dto;
        }

        public async Task<List<AccountDto>> GetListAsync()
        {
            var cached = await _accountsCache.GetAsync(AccountCacheKeys.ListKey);

            if (cached != null)
            {
                return cached;
            }

            var accounts = await _accountRepository.GetListAsync();
            var mapped = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);

            await Task.Delay(10000);

            await _accountsCache.SetAsync(AccountCacheKeys.ListKey, mapped, _defaultCacheOptions);

            return mapped;
        }

        public async Task<List<AccountDto>> GetLast10CreatedAccountAsync()
        {
            var key = AccountCacheKeys.Last10Key;
            var cached = await _accountsCache.GetAsync(key);
            if (cached != null)
            {
                return cached;
            }
            var accounts = await _accountRepository.GetLast10CreatedAccountAsync();
            var mapped = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
            await Task.Delay(10000);

            await _accountsCache.SetAsync(key, mapped, _defaultCacheOptions);
            return mapped;
        }

        public async Task<List<AccountDto>> GetAccountByCustomerId(Guid customerId)
        {
            var key = AccountCacheKeys.ListByCustomer(customerId);
            var cached = await _accountsCache.GetAsync(key);
            if (cached != null)
            {
                await Task.Delay(10000);
                return cached;
            }

            var accounts = await _accountRepository.GetListAsync(x => x.CustomerId == customerId);
            if (accounts == null || accounts.Count == 0)
            {
                throw new UserFriendlyException($"No account found for customerId '{customerId}'.");
            }

            var mapped = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
            await _accountsCache.SetAsync(key, mapped, _defaultCacheOptions);

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

                if (previousCustomerId != account.CustomerId)
                {
                    await _accountsCache.RemoveAsync(AccountCacheKeys.ListByCustomer(previousCustomerId));
                }
                await _accountsCache.RemoveAsync(AccountCacheKeys.ListByCustomer(account.CustomerId));

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
                // Ignore if not found
            }

            await _accountRepository.DeleteAsync(id);

            await InvalidateCachesAfterWriteAsync(new[] { id });

            if (account != null)
            {
                await _accountsCache.RemoveAsync(AccountCacheKeys.ListByCustomer(account.CustomerId));
            }

            Logger.LogInformation("Account deleted (Id: {AccountId})", id);
            return true;
        }
    }
}
