using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;

public class PlayCard
{
    private HandView _handView;
    private readonly List<IPlayCardStrategy> _playCardStrategies;

    public PlayCard(GameBoard gameBoard,
        HandView handView,
        DiscardCard discardCard,
        Action<Action<bool>> isShieldUsed)
    {
        _handView = handView;
        _playCardStrategies = new List<IPlayCardStrategy>
        {
            new FamilyMemberStrategy(gameBoard),
            new SabotageStrategy(gameBoard, discardCard, isShieldUsed)
        };
    }

    public void Execute(Card selectedCard, 
        Player player,
        PlayerHand hand,
        GenerationRow rowSelected)
    {
        if (rowSelected == GenerationRow.None)
            return;

        RemoveCardFromHand(selectedCard, hand);
        _handView.RemoveCard(selectedCard);
        foreach (var strategy in _playCardStrategies.Where(strategy => strategy.Is(selectedCard)))
            strategy.Execute(selectedCard, player, rowSelected);
    }

    private static void RemoveCardFromHand(Card selectedCard, PlayerHand hand)
    {
        var domainCard = hand.Cards.First(x => x.Name == selectedCard.Name);
        hand.Cards.Remove(domainCard);
    }
}

public class SabotageStrategy : IPlayCardStrategy
{
    private readonly GameBoard _gameBoard;
    private readonly DiscardCard _discardCard;
    private readonly Action<Action<bool>> _isShieldUsed;

    public SabotageStrategy(GameBoard gameBoard,
        DiscardCard discardCard,
        Action<Action<bool>> isShieldUsed)
    {
        _gameBoard = gameBoard;
        _discardCard = discardCard;
        _isShieldUsed = isShieldUsed;
    }

    public bool Is(Card card) => card.GetType() == typeof(SabotageCard);

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _isShieldUsed(result =>
        {
            if (result) _gameBoard.RemoveAllCardsFromOpponent(player, row);
        });
        _discardCard.Execute(card);
        Debug.Log("Sabotage completed");
    }
}