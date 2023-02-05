namespace Actions
{
    public class DrawCard
    {
        private readonly Deck _deck;
        private readonly AddDiscardPileToDeck _addDiscardPileToDeck;

        public DrawCard(Deck deck, AddDiscardPileToDeck addDiscardPileToDeck)
        {
            _deck = deck;
            _addDiscardPileToDeck = addDiscardPileToDeck;
        }

        public Card Execute(Player player)
        {
            if (_deck.Cards.Count == 0)
                _addDiscardPileToDeck.Execute();
            var card = _deck.Cards.Pop();
            player.PlayerHand.Cards.Add(card);
            return card;
        }
    }
}