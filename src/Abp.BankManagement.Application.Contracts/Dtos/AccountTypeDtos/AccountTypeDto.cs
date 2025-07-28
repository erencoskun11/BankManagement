using System;

namespace Abp.BankManagement.Dtos.AccountTyoeDtos
{
    public class AccountTypeDto
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
    }
}
