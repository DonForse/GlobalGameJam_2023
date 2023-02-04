namespace Actions
{
    public class AddDiscardPileToDeck
    {
        private readonly DiscardPile _discardPile;
        private readonly Deck _deck;
        private readonly ShuffleDeck _shuffleDeck;
        
        public AddDiscardPileToDeck(DiscardPile discardPile, Deck deck, ShuffleDeck shuffleDeck)
        {
            _discardPile = discardPile;
            _deck = deck;
            _shuffleDeck = shuffleDeck;
        }
        
        public void Execute()
        {
            foreach (var card in _discardPile.Cards)
                _deck.Cards.Push(card);
            _shuffleDeck.Execute();
        }
    }
}