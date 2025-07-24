using System.Collections.Generic;

namespace Abp.BankManagement.Entities;

public class AccountType : LookupBaseEntity
{
    public virtual ICollection<Account> Accounts { get; set; }
}