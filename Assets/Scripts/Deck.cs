using System.Collections.Generic;

public class Deck{
    private readonly CardRepositoryScriptableObject _cardRepository;

    public Stack<Card> Cards = new();

    public Deck(CardRepositoryScriptableObject cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public void Initialize() => AddCardsToDeck();
    private void AddCardsToDeck()
    {
        AddCardOfType(_cardRepository.FamilyMemberCards);
        AddCardOfType(_cardRepository.PrincipalOjectiveCards);
        AddCardOfType(_cardRepository.DeckObjectiveCard);
        AddCardOfType(_cardRepository.SabotageCards);
        AddCardOfType(_cardRepository.ShieldCards);
    }
    private void AddCardOfType(IEnumerable<Card> cards)
    {
        foreach (var card in cards) AddCards(card);
    }
    private void AddCards(Card card) => Cards.Push(card);
}