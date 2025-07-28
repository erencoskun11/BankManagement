using System;

namespace Abp.BankManagement.Dtos
{
    public class CardTypeDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
    }
}
