using System.Collections.Generic;
using Cards;

namespace Game
{
    public class ObjectiveDeck
    {
        private readonly PrincipalObjectiveCardRepositoryScriptableObject _cardRepository;

        public Stack<ObjectiveCard> Cards = new();

        public ObjectiveDeck(PrincipalObjectiveCardRepositoryScriptableObject cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public void Initialize() => AddCardsToDeck();
        private void AddCardsToDeck() => AddCardOfType(_cardRepository.PrincipalOjectiveCards);

        private void AddCards(ObjectiveCard card) => AddACard(card);

        public void AddACard(ObjectiveCard card)
        {
            for (int i = 0; i < card.DeckInitialAmount; i++)
                Cards.Push(new ObjectiveCard
                {
                    Drawing = card.Drawing,
                    Name = card.Name,
                    DeckInitialAmount = card.DeckInitialAmount,
                    GrandParents = card.GrandParents,
                    Parents= card.Parents,
                    Children = card.Children
                    
                });
        }
    
        private void AddCardOfType(IEnumerable<ObjectiveCard> cards)
        {
            foreach (var card in cards) AddCards(card);
        }
    }
}