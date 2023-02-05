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
        GenerationRow rowSelected)
    {
        if (rowSelected == GenerationRow.None)
            return;

        RemoveCardFromHand(selectedCard, player.PlayerHand);
        _handView.RemoveCard(selectedCard);
        foreach (var strategy in _playCardStrategies
                     .Where(strategy => strategy.Is(selectedCard) 
                        && strategy.CanPlay(selectedCard, player)))
            strategy.Execute(selectedCard, player, rowSelected);
    }

    private static void RemoveCardFromHand(Card selectedCard, PlayerHand hand)
    {
        var domainCard = hand.Cards.First(x => x.Name == selectedCard.Name);
        hand.Cards.Remove(domainCard);
    }
}