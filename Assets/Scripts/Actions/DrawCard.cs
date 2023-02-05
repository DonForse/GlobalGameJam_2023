namespace Actions
{
    public class DrawCard
    {
        private Deck _deck;
        private AddDiscardPileToDeck _addDiscardPileToDeck;

        public DrawCard(Deck deck, AddDiscardPileToDeck addDiscardPileToDeck)
        {
            _deck = deck;
            _addDiscardPileToDeck = addDiscardPileToDeck;
        }

        public void Execute(Player player)
        {
            if (_deck.Cards.Count == 0)
                _addDiscardPileToDeck.Execute();
            var card = _deck.Cards.Pop();
            player.PlayerHand.Cards.Add(card);
        }
    }
}