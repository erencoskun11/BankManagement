namespace Abp.BankManagement.ExceptionCodes
{
    public class AccountExceptionCodes
    {
        public const string NotFoundException = "Account.NotFound";
        public const string AccountNumberAlreadyExists = "Account.AccountNumberAlreadyExists";
        public const string InvalidIBAN = "Account.InvalidIBAN";
        public const string InactiveAccountCannotBeUpdated = "Account.InactiveAccountCannotBeUpdated";
    }
}
