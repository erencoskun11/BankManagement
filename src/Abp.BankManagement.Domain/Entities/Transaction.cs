using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.BankManagement.Entities;

public class Transaction : FullAuditedEntity<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime TransactionDate { get; set; }

    public Guid AccountId { get; set; }               
    public virtual Account Account { get; set; }      

    public Guid? CardId { get; set; }
    public virtual Card? Card { get; set; }

    public Guid TransactionTypeId { get; set; }
    public virtual TransactionType? TransactionType { get; set; }

    public Transaction(
        decimal amount,
        string? description,
        Guid? cardId,
        Guid transactionTypeId,
        Guid accountId,
        DateTime? transactionDate = null)
    {
        Amount = amount;
        Description = description;
        CardId = cardId;
        TransactionTypeId = transactionTypeId;
        AccountId = accountId;
        TransactionDate = transactionDate ?? DateTime.UtcNow;
    }

    public Transaction() { }
}
