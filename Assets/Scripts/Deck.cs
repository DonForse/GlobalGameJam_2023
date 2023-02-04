using System.Collections.Generic;

public class Deck{
    private readonly CardRepositoryScriptableObject _cardRepository;

    public Deck(CardRepositoryScriptableObject cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public Stack<Card> Cards;
}