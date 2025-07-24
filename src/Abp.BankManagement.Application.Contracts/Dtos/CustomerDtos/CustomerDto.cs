using System;

namespace Abp.BankManagement.Dtos.CustomerDtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal RiskLimit { get; set; }

        public int AccountCount { get; set; }  // Opsiyonel: kaç hesabı var
    }
}
