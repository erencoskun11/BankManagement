// ... (tüm using ifadelerin zaten doğru, o yüzden olduğu gibi bırakılıyor)

namespace Abp.BankManagement.Services
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        // ... (diğer alanlar ve constructor olduğu gibi kalıyor)

        // ... (diğer metodlar: PublishCreatedEtoAsync, MapToDto, InvalidateCachesAfterWriteAsync, CreateAsync, BulkCreateAsync, GetAsync, GetListAsync)

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
