using System;
using System.Collections.Generic;
using System.Linq;

namespace Actions
{
    public class PlayerHand
    {
        public List<Card> Cards = new ();
        public Card GetCard(Type typeOfCard) => Cards.First(card => card.GetType() == typeOfCard);
    }
}