using System;
using System.Collections.Generic;
using System.Linq;

namespace Actions
{
    public class PlayerHand
    {
        public List<Card> Cards = new ();
        public Card GetShieldCard() => Cards.First(card => card.Name.ToLowerInvariant() == "anulo mufa");
    }
}