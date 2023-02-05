using System;
using UnityEngine;
using UnityEngine.Events;

public class TurnService
{
    private readonly GameView _gameView;
    private readonly PlayCard _playCard;
    private int turn = 0;
    private PlayerEnum playerTurn = PlayerEnum.Player;
    private int _cardsPlayed = 0;

    public TurnService(GameView gameView, PlayCard playCard)
    {
        _gameView = gameView;
        _playCard = playCard;

        _playCard.OnCardPlayed.AddListener(AddCardPlayed);
    }

    public UnityEvent<PlayerEnum> OnTurnChange = new UnityEvent<PlayerEnum>();
    private void AddCardPlayed()
    {
        _cardsPlayed++;
        if (_cardsPlayed >= 2)
        {
            ChangeTurn();
        }
    }

    private void ChangeTurn()
    {
        Debug.Log("change turn");
        var newPlayer = playerTurn == PlayerEnum.Player ? PlayerEnum.Npc : PlayerEnum.Player;
        playerTurn = newPlayer;
        StartTurn(newPlayer);
        OnTurnChange.Invoke(newPlayer);
    }

    public void StartTurn(PlayerEnum player)
    {
        playerTurn = player;
        _gameView.ShowTurn(player);
        turn++;
        _cardsPlayed = 0;
    }

    public PlayerEnum GetTurn() => playerTurn;
    
}