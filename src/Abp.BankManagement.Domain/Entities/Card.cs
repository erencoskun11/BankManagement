using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.BankManagement.Entities;

public class Card : FullAuditedEntity<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; } 

    public string CardNumber { get; set; }

    public int ExpiryMonth { get; set; }

    public int ExpiryYear { get; set; }

    public string CCV { get; set; }

    public bool IsActive { get; set; }

    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }
    
    public Guid CardTypeId { get; set; }
    public virtual CardType CardType { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

    public Card(
            string cardNumber,
            int expiryMonth,
            int expiryYear,
            string ccv,
            Guid accountId,
            Guid cardTypeId,
            bool isActive = true)
    {
        CardNumber = cardNumber;
        ExpiryMonth = expiryMonth;
        ExpiryYear = expiryYear;
        CCV = ccv;
        AccountId = accountId;
        CardTypeId = cardTypeId;
        IsActive = isActive;
    }

    public Card() { }
}


