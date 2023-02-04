namespace Actions
{
    public class DiscardCard
    {
        private DiscardPile _discardPile;

        public DiscardCard(DiscardPile discardPile)
        {
            _discardPile = discardPile;
        }

        public void Execute(Card card)
        {
            _discardPile.Cards.Add(card);
        }
    }
}