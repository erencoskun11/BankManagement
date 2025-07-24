using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Abp.BankManagement.Entities;

public abstract class LookupBaseEntity : IEntity<Guid>, IMultiTenant, ICreationAuditedObject
{
    public Guid Id { get; }
    public Guid? TenantId { get; }
    public DateTime CreationTime { get; }
    public Guid? CreatorId { get; }
    public int? Code { get; set; }
    public string Name { get; set; }

    public object?[] GetKeys()
    {
        return new object?[] { Id };
    }
}