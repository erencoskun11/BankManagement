using System.Collections.Generic;

namespace Abp.BankManagement.Entities;

public class TransactionType : LookupBaseEntity
{
    public virtual ICollection<Transaction> Transactions { get; set; }
}

