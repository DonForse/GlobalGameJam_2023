using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using Cards.Drag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    private HasShield _hasShield;
    private ShowPromptToUseShield _showPromptToUseShield;
    private ShieldView _shieldView;

    void Start()
    {
        _handView = _handView.WithOnCardSelected(CardStartDrag,CardEndDrag);

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
        _hasShield = new HasShield();
        _showPromptToUseShield = new ShowPromptToUseShield();
        _playCard = new PlayCard(_gameBoard, _handView, _discardCard, OnShield);
        
        _shuffleDeck.Execute();
        _drawCard.Execute(_playerHand);
        _drawCard.Execute(_playerHand);
        _drawCard.Execute(_playerHand);
        _drawCard.Execute(_playerHand);

        foreach (var card in _playerHand.Cards)
            _handView.AddCard(card);
    }

    private void OnShield(Action<bool> callBack)
    {
        if (_hasShield.Execute(_playerHand))
            _shieldView.OnShieldCalled(_playCard, _player, _gameBoard, callBack);
        else
            callBack(false);
    }

    private void CardStartDrag(OverlayCardView selectedCard)
    {
        Debug.Log($"Start card drag: {selectedCard.name}");
        var card = cardsRepo.GetFromId(selectedCard.name);
        DraggingService.StartDragging(card, () => CardEndDrag(selectedCard));
    }
    private void CardEndDrag(OverlayCardView selectedCard)
    {
        Debug.Log($"End card drag{selectedCard.name}"); 
        var card = cardsRepo.GetFromId(selectedCard.name);
        //Blas Aca nos da la posicion.
        _playCard.Execute(card, _player, _playerHand, GenerationRow.Parent);
    }
}

public class ShieldView : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    public void OnShieldCalled(PlayCard playCard, Player player, GameBoard gameBoard, Action<bool> callBack)
    {
        yesButton.onClick.AddListener(() =>
        {
            callBack(true);
            yesButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(() =>
        {
            callBack(false);
            yesButton.onClick.RemoveAllListeners();
        });
    }
}

public class HasShield
{
    public bool Execute(PlayerHand playerHand) => 
        playerHand.Cards.Exists(x => x.GetType() == typeof(ShieldCard));
}