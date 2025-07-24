using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.BankManagement.Entities;

public class Account : FullAuditedEntity<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }  // IMultiTenant için zorunlu

    public string AccountName { get; set; }

    public string AccountNumber { get; set; }

    public string IBAN { get; set; }

    public DateTime OpenedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public  virtual ICollection<Card> Cards { get; set; }  

    public ICollection<Transaction> Transactions { get; set; }
    public Account() { }

    public Account(DateTime openedAt,bool isActive)
    {
        OpenedAt = openedAt;
        IsActive = isActive;
    }
}

