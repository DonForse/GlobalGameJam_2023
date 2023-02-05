using Actions;
using Cards.Drag;
using UnityEngine;

public class GameApplication : MonoBehaviour
{
    [SerializeField] private CardRepositoryScriptableObject cardsRepo;
    [SerializeField] private HandView _handView;
    private AddDiscardPileToDeck _addDiscardPileToDeck;
    private DrawCard _drawCard;
    private PlayerHand _playerHand;
    private ShuffleDeck _shuffleDeck;
    private Deck _deck;
    private DiscardPile _discardPile;
    private GameBoard _gameBoard;
    private PlayCard _playCard;
    private Player _player;
    private Player _npc;
    private DiscardCard _discardCard;

    void Start()
    {
        _handView = _handView.WithOnCardSelected(CardStartDrag, _ => { });

        _player = new Player();
        _npc = new Player();
        
        _deck = new Deck(cardsRepo);
        _deck.Initialize();

        _gameBoard = new GameBoard();
        _gameBoard.Initialize(_player, _npc);

        _playerHand = new PlayerHand();
        _shuffleDeck = new ShuffleDeck(_deck);
        _discardPile = new DiscardPile();
        _discardCard = new DiscardCard(_discardPile);
        _addDiscardPileToDeck = new AddDiscardPileToDeck(_discardPile, _deck, _shuffleDeck);
        _drawCard = new DrawCard(_deck,_addDiscardPileToDeck);
        _playCard = new PlayCard(_gameBoard, _handView, _discardCard);
        
        _shuffleDeck.Execute();
        _drawCard.Execute(_playerHand);
        _drawCard.Execute(_playerHand);
        _drawCard.Execute(_playerHand);
        _drawCard.Execute(_playerHand);

        foreach (var card in _playerHand.Cards)
            _handView.AddCard(card);
    }

    private void CardStartDrag(OverlayCardView selectedCard)
    {
        Debug.Log($"Start card drag: {selectedCard.name}");
        var card = cardsRepo.GetFromId(selectedCard.name);
        DraggingService.StartDragging(card, generationRow => CardEndDrag(selectedCard, generationRow));
    }
    private void CardEndDrag(OverlayCardView selectedCard, GenerationRow generationRow)
    {
        Debug.Log($"End card drag {selectedCard.name}");
        var card = cardsRepo.GetFromId(selectedCard.name);
        _playCard.Execute(card, _player, _playerHand, generationRow);
    }
}