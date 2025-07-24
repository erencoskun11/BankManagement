using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Abp.BankManagement.Etos.CustomerEtos
{
    public class CustomerEto : EtoBase
    {
        public Guid CustomerId { get; set; }

        public Guid? TenantId { get; set; }

        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal RiskLimit { get; set; }
    }
}