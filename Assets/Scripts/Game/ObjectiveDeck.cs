using System.Collections.Generic;

namespace Game
{
    public class ObjectiveDeck
    {
        private readonly PrincipalObjectiveCardRepositoryScriptableObject _cardRepository;

        public Stack<Card> Cards = new();

        public ObjectiveDeck(PrincipalObjectiveCardRepositoryScriptableObject cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public void Initialize() => AddCardsToDeck();
        private void AddCardsToDeck() => AddCardOfType(_cardRepository.PrincipalOjectiveCards);

        private void AddCards(Card card) => AddACard(card);

        public void AddACard(Card card)
        {
            for (int i = 0; i < card.DeckInitialAmount; i++)
                Cards.Push(new Card
                {
                    Drawing = card.Drawing,
                    Name = card.Name,
                    DeckInitialAmount = card.DeckInitialAmount
                });
        }
    
        private void AddCardOfType(IEnumerable<Card> cards)
        {
            foreach (var card in cards) AddCards(card);
        }
    }
}