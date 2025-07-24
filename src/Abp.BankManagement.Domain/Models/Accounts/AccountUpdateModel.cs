namespace Abp.BankManagement.Models.Accounts
{
    public class AccountUpdateModel
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public bool IsActive { get; set; }
    }
}
