using System;

namespace Abp.BankManagement.Models.Customers
{
    public class CustomerCreateModel
    {
        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal RiskLimit { get; set; } = 10000;
    }
}
