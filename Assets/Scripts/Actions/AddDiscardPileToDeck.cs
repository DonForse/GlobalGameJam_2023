namespace Actions
{
    public class AddDiscardPileToDeck
    {
        private readonly DiscardPile _discardPile;
        private readonly Deck _deck;
        private readonly Shuffle _shuffle;
        
        public AddDiscardPileToDeck(DiscardPile discardPile, Deck deck, Shuffle shuffle)
        {
            _discardPile = discardPile;
            _deck = deck;
            _shuffle = shuffle;
        }
        
        public void Execute()
        {
            foreach (var card in _discardPile.Cards)
                _deck.Cards.Push(card);
            _shuffle.Execute();
        }
    }
}