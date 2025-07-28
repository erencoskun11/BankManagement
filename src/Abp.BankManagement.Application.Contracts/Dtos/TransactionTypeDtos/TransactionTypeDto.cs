using System;

namespace Abp.BankManagement.Dtos.TransactionTypeDtos
{
    public class TransactionTypeDto
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
    }
}
