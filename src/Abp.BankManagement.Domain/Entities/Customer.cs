using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.BankManagement.Entities
{
    public class Customer : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal RiskLimit { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public Customer(
            Guid? tenantId,
                string fullName,
                string nationalId,
                string birthPlace,
                DateTime birthDate,
                decimal riskLimit)
        {
            TenantId = tenantId;
            FullName = fullName;
            NationalId = nationalId;
            BirthPlace = birthPlace;
            BirthDate = birthDate;
            RiskLimit = riskLimit;
        }

        // EF Core için parametresiz constructor
        public Customer()
        {
        }
    }
}
