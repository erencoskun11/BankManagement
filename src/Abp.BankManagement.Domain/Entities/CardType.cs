using System;
using System.Collections.Generic;

namespace Abp.BankManagement.Entities;

public class CardType : LookupBaseEntity
{
    public CardType()
    {
        Cards = new HashSet<Card>();

    }
    public virtual ICollection<Card> Cards { get; set; }
}


