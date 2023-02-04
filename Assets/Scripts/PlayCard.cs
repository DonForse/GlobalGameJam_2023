using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;

public class PlayCard
{
    private HandView _handView;
    private GameBoard _gameBoard;
    private readonly List<IPlayCardStrategy> _playCardStrategies;

    public PlayCard(GameBoard gameBoard, HandView handView)
    {
        _gameBoard = gameBoard;
        _handView = handView;
        _playCardStrategies = new List<IPlayCardStrategy>
        {
            new FamilyMemberStrategy(_gameBoard),
            new SabotageStrategy()
        };
    }

    public void Execute(Card selectedCard,Player player, PlayerHand hand, GenerationRow rowSelected)
    {
        if (rowSelected == GenerationRow.None)
            return;

        RemoveCardFromHand(selectedCard, hand);
        _handView.RemoveCard(selectedCard);
        foreach (var strategy in _playCardStrategies)
        {
            if (!strategy.Is(selectedCard)) continue;
            strategy.Execute(selectedCard, player, rowSelected );
        }
    }

    private static void RemoveCardFromHand(Card selectedCard, PlayerHand hand)
    {
        var domainCard = hand.Cards.First(x => x.Name == selectedCard.Name);
        hand.Cards.Remove(domainCard);
    }
}

public class SabotageStrategy : IPlayCardStrategy
{
    public bool Is(Card card) => card.GetType() == typeof(SabotageCard);

    public void Execute(Card card, Player player, GenerationRow row)
    {
        Debug.Log("card");
    }
}