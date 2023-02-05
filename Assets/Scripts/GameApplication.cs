using System;
using Actions;
using Cards.Drag;
using Game;
using UnityEngine;

public class GameApplication : MonoBehaviour
{
    [SerializeField] private CardRepositoryScriptableObject cardsRepo;
    [SerializeField] private PrincipalObjectiveCardRepositoryScriptableObject _principalObjectiveCardRepo;
    [SerializeField] private HandView _handView;
    [SerializeField] private GameView gameView;
    [SerializeField] private BotService _botService;
    [SerializeField] private ShieldView shieldView;
    [SerializeField] private PrincipalObjectiveCardsView _principalObjectivesCardsView;
    private AddDiscardPileToDeck _addDiscardPileToDeck;
    private DrawCard _drawCard;
    private ShuffleDeck _shuffleDeck;
    private Deck _deck;
    private ObjectiveDeck _principalObjectivesDeck;
    private DiscardPile _discardPile;
    private GameBoard _gameBoard;
    private PlayCard _playCard;
    private Player _player;
    private Player _npc;
    private DiscardCard _discardCard;
    private HasShield _hasShield;
    private TurnService _turnService;
    private CanClaimTrophy _canClaimTrophy;
    private GetPlayerFromTurn _getPlayerFromTurn;
    private ClaimTrophy _claimTrophy;

    void Start()
    {
        _player = new Player(false)
        {
            PlayerHand = new PlayerHand()
        };
        _npc = new Player(true)
        {
            PlayerHand = new PlayerHand()
        };

        _principalObjectivesDeck = new ObjectiveDeck(_principalObjectiveCardRepo);
        _principalObjectivesDeck.Initialize();

        _deck = new Deck(cardsRepo);
        _deck.Initialize();
        _discardPile = new DiscardPile();
        _gameBoard = new GameBoard();

        _discardCard = new DiscardCard(_discardPile);
        _shuffleDeck = new ShuffleDeck(_deck);
        _addDiscardPileToDeck = new AddDiscardPileToDeck(_discardPile, _deck, _shuffleDeck);
        _drawCard = new DrawCard(_deck, _addDiscardPileToDeck);
        _hasShield = new HasShield();
        _playCard = new PlayCard(_gameBoard, _discardCard, OnShield);
        _turnService = new TurnService(gameView, _playCard);
        _getPlayerFromTurn = new GetPlayerFromTurn(_turnService, _player, _npc);
        _canClaimTrophy = new CanClaimTrophy(_gameBoard, _getPlayerFromTurn);
        _claimTrophy = new ClaimTrophy(_gameBoard, _getPlayerFromTurn);
        _botService = _botService.With(_turnService, _playCard, _npc, _canClaimTrophy, _claimTrophy);

        _gameBoard.Initialize(_player, _npc, _discardCard);
        _handView = _handView.WithTurnService(_turnService);
        _handView = _handView.WithOnCardSelected(CardStartDrag, _ => { });

        ObjectiveService.Initialize(_principalObjectivesDeck);
        TrophiesService.SomeoneWon.AddListener(gameView.WinGame);

        InitialGameSetUp();
        AddHandCardsVisually();
        AddPrincipalObjectiveCardsVisually();
        _turnService.StartTurn(PlayerEnum.Player);
        _turnService.OnTurnChange.AddListener(DrawNewCards);
    }

    private void DrawNewCards(PlayerEnum playerEnum)
    {
        if (playerEnum == PlayerEnum.Npc)
        {
            while (_npc.PlayerHand.Cards.Count < 5) _drawCard.Execute(_npc);
        }
        else
        {
            while (_player.PlayerHand.Cards.Count < 5)
            {
                var card = _drawCard.Execute(_player);
                _handView.AddCard(card);
            }
        }
    }


    private void AddPrincipalObjectiveCardsVisually() =>
        _principalObjectivesCardsView.Init(_canClaimTrophy, _claimTrophy);

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
        _handView.Show();
        Debug.Log($"End card drag {selectedCard.name}");
        var card = cardsRepo.GetFromId(selectedCard.name);
        if (_playCard.Execute(card, _player, generationRow))
            _handView.RemoveCard(card);
    }
}

public class HasShield
{
    public bool Execute(PlayerHand playerHand) =>
        playerHand.Cards.Exists(x => x.Name.ToLowerInvariant()== "anulo mufa");
}

public class GetPlayerFromTurn
{
    private readonly TurnService _turnService;
    private readonly Player _player;
    private readonly Player _npc;

    public GetPlayerFromTurn(TurnService turnService, Player player, Player npc)
    {
        _turnService = turnService;
        _player = player;
        _npc = npc;
    }

    public Player Execute() => _turnService.GetTurn() == PlayerEnum.Player ? _player : _npc;
}

public class ClaimTrophy
{
    private readonly GameBoard _gameBoard;
    private readonly GetPlayerFromTurn _getPlayerFromTurn;

    public ClaimTrophy(GameBoard gameBoard, GetPlayerFromTurn getPlayerFromTurn)
    {
        _gameBoard = gameBoard;
        _getPlayerFromTurn = getPlayerFromTurn;
    }

    public void Execute(string cardId)
    {
        var player = _getPlayerFromTurn.Execute();
        _gameBoard.CompletePrincipalObjectiveCard(cardId, player);
        TrophiesService.AddTrophy(player);
        _gameBoard.AddPoint(player);
        ObjectiveService.Claim(cardId);
    }
}