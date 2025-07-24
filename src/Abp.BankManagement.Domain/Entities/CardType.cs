using System.Collections.Generic;

namespace Abp.BankManagement.Entities;

public class CardType : LookupBaseEntity
{
    public virtual ICollection<Card> Cards { get; set; }
}


