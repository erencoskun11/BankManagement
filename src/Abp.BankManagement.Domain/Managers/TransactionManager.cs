using Abp.BankManagement.Entities;
using Abp.BankManagement.Models.Transactions;
using Volo.Abp.Domain.Services;

namespace Abp.BankManagement.Managers
{
    
        public class TransactionManager : DomainService
        {
            public Transaction Create(TransactionCreateModel model)
            {
                return new Transaction(
                    model.Amount,
                    model.Description,
                    model.CardId,
                    model.TransactionTypeId,
                    model.AccountId,
                    model.TransactionDate
                );
            }

            public Transaction Update(Transaction transaction, TransactionUpdateModel model)
            {
                transaction.Amount = model.Amount;
                transaction.Description = model.Description;
                transaction.CardId = model.CardId;
                transaction.TransactionTypeId = model.TransactionTypeId;
                if (model.TransactionDate.HasValue)
                    transaction.TransactionDate = model.TransactionDate.Value;

                return transaction;
            }
        }
}
