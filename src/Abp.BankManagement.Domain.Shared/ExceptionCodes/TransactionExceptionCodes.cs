namespace Abp.BankManagement.ExceptionCodes
{
    public class TransactionExceptionCodes
    {
        public const string NotFoundException = "Transaction.NotFound";
        public const string InvalidAmount = "Transaction.InvalidAmount";
        public const string AccountNotActie = "Transaction.AccountNotActive";
        public const string CardNotActive = "Transaction.CardNotActive";
        public const string TransactionDateInvalid = "Transaction.TransactionDateInvalid";
        public const string ExceedsRiskLimit = "Transaction.ExceedsRiskLimit";
    }
}
