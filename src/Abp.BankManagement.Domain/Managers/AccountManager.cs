using Abp.BankManagement.Models.Accounts;
using System;
using Abp.BankManagement.Entities;
using Volo.Abp.Domain.Services;

namespace Abp.BankManagement.Managers
{
    public class AccountManager : DomainService
    {
        public Account Create(AccountCreateModel model)
        {
            return new Account
            {
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
                IBAN = model.IBAN,
                OpenedAt = model.OpenedAt ?? DateTime.UtcNow,
                IsActive = model.IsActive,
                CustomerId = model.CustomerId
            };
        }

        public Account Update(Account account, AccountUpdateModel model)
        {
            account.AccountName = model.AccountName;
            account.AccountNumber = model.AccountNumber;
            account.IBAN = model.IBAN;
            account.IsActive = model.IsActive;

            return account;
        }
    }
}

