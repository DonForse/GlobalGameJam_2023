using Actions;
using UnityEngine;

public class GameApplication : MonoBehaviour
{
    [SerializeField] private CardRepositoryScriptableObject cardsRepo;
    [SerializeField] private HandView _handView;

    void Start()
    {
        _handView = _handView.WithOnCardSelected(CardStartDrag,CardEndDrag);
        
        var deck = new Deck(cardsRepo);
        deck.Initialize();
        var shuffleDeck = new ShuffleDeck(deck);
        shuffleDeck.Execute();
        var addDiscardPileToDeck = new AddDiscardPileToDeck(new DiscardPile(), deck, shuffleDeck);
        var drawCard = new DrawCard(deck,addDiscardPileToDeck);
        var playerHand = new PlayerHand();
        drawCard.Execute(playerHand);
        drawCard.Execute(playerHand);
        drawCard.Execute(playerHand);
        drawCard.Execute(playerHand);

        foreach (var card in playerHand.Cards)
            _handView.AddCard(card);
    }

    private void CardStartDrag(OverlayCardView selectedCard)
    {
        Debug.Log($"{selectedCard.name}");
    }
    private void CardEndDrag(OverlayCardView selectedCard)
    {
        Debug.Log($"{selectedCard.name}");
    }
}