using Abp.BankManagement.Entities;
using Abp.BankManagement.Models.Cards;
using Volo.Abp.Domain.Services;

namespace Abp.BankManagement.Managers
{
    public class CardManager : DomainService
    {
        public Card Create(CardCreateModel model)
        {
            return new Card(
                model.CardNumber,
                model.ExpiryMonth,
                model.ExpiryYear,
                model.CCV,
                model.AccountId,
                model.CardTypeId,
                model.IsActive
            );
        }

        public Card Update(Card card, CardUpdateModel model)
        {
            card.CardNumber = model.CardNumber;
            card.ExpiryMonth = model.ExpiryMonth;
            card.ExpiryYear = model.ExpiryYear;
            card.CCV = model.CCV;
            card.CardTypeId = model.CardTypeId;
            card.IsActive = model.IsActive;

            return card;
        }
    }
}
