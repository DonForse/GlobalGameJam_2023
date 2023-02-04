﻿using System.Collections.Generic;
using System.Linq;
using Actions;

public class PlayCard
{
    private HandView _handView;
    private readonly List<IPlayCardStrategy> _playCardStrategies;

    public PlayCard(GameBoard gameBoard, HandView handView, DiscardCard discardCard)
    {
        _handView = handView;
        _playCardStrategies = new List<IPlayCardStrategy>
        {
            new FamilyMemberStrategy(gameBoard),
            new SabotageStrategy(gameBoard, discardCard)
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