using System.Linq;
using UnityEngine;

namespace Actions
{
    public class ShuffleDeck
    {
        private Deck _deck;

        public ShuffleDeck(Deck deck)
        {
            _deck = deck;
        }

        public void Execute()
        {
            var shuffledCards = _deck.Cards.OrderBy(x => Random.Range(0, 1000)).ToList();
            _deck.Cards.Clear();
            foreach (var card in shuffledCards)
                _deck.Cards.Push(card);
        }
    }
}