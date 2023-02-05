using System;
using Actions;
using Cards.Drag;
using UnityEngine;

public class GameApplication : MonoBehaviour
{
    [SerializeField] private CardRepositoryScriptableObject cardsRepo;
    [SerializeField] private HandView _handView;
    [SerializeField] private GameView gameView;
    [SerializeField] private ShieldView shieldView;
    private AddDiscardPileToDeck _addDiscardPileToDeck;
    private DrawCard _drawCard;
    private ShuffleDeck _shuffleDeck;
    private Deck _deck;
    private DiscardPile _discardPile;
    private GameBoard _gameBoard;
    private PlayCard _playCard;
    private Player _player;
    private Player _npc;
    private DiscardCard _discardCard;
    private HasShield _hasShield;
    private TurnService _turnService;
    private BotService _botService;

    void Start()
    {
        _player = new Player()
        {
            PlayerHand = new PlayerHand()
        };
        _npc = new Player()
        {
            PlayerHand = new PlayerHand()
        };

        _deck = new Deck(cardsRepo);
        _deck.Initialize();

        _gameBoard = new GameBoard();
        _gameBoard.Initialize(_player, _npc);
        _shuffleDeck = new ShuffleDeck(_deck);
        _discardPile = new DiscardPile();
        _discardCard = new DiscardCard(_discardPile);
        _addDiscardPileToDeck = new AddDiscardPileToDeck(_discardPile, _deck, _shuffleDeck);
        _drawCard = new DrawCard(_deck,_addDiscardPileToDeck);
        _hasShield = new HasShield();
        new ShowPromptToUseShield();
        _playCard = new PlayCard(_gameBoard, _discardCard, OnShield);
        _turnService = new TurnService(gameView, _playCard);
        _botService = new BotService(_turnService);
        _handView = _handView.WithOnCardSelected(CardStartDrag, _ => { });
        _handView = _handView.WithTurnService(_turnService);
        
        InitialGameSetUp();
        AddHandCardsVisually();
        _turnService.StartTurn(PlayerEnum.Player);
    }

    private void AddHandCardsVisually()
    {
        foreach (var card in _player.PlayerHand.Cards)
            _handView.AddCard(card);
    }

    private void InitialGameSetUp()
    {
        _shuffleDeck.Execute();
        _drawCard.Execute(_player);
        _drawCard.Execute(_player);
        _drawCard.Execute(_player);
        _drawCard.Execute(_player);
        _drawCard.Execute(_player);

        _drawCard.Execute(_npc);
        _drawCard.Execute(_npc);
        _drawCard.Execute(_npc);
        _drawCard.Execute(_npc);
        _drawCard.Execute(_npc);
    }

    private void OnShield(Action<bool> callBack)
    {
        if (_hasShield.Execute(_player.PlayerHand))
            shieldView.OnShieldCalled(_playCard, _player, callBack);
        else
            callBack(false);
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
        _playCard.Execute(card, _player, generationRow);
    }
}

public class HasShield
{
    public bool Execute(PlayerHand playerHand) => 
        playerHand.Cards.Exists(x => x.GetType() == typeof(ShieldCard));
}