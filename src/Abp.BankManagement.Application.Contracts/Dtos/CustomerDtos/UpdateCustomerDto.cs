using System;

namespace Abp.BankManagement.Dtos.CustomerDtos
{
    public class UpdateCustomerDto
    {
        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal RiskLimit { get; set; }
    }
}
