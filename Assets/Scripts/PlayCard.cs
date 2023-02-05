using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine.Events;

public class PlayCard
{
    public UnityEvent OnCardPlayed = new UnityEvent();
    private readonly List<IPlayCardStrategy> _playCardStrategies;


    public PlayCard(GameBoard gameBoard,
        DiscardCard discardCard,
        Action<Action<bool>> isShieldUsed)
    {
        _playCardStrategies = new List<IPlayCardStrategy>
        {
            new FamilyMemberStrategy(gameBoard),
            new SabotageStrategy(gameBoard, discardCard, isShieldUsed),
            new ShieldStrategy(discardCard),
            new DeckObjectiveStrategy(gameBoard),
        };
        foreach (var strategy in _playCardStrategies)
        {
            strategy.CardPlayed.AddListener(CardPlayed);
        }
    }

    private void CardPlayed()
    {
        OnCardPlayed?.Invoke();
    }

    public bool Execute(Card selectedCard, 
        Player player,
        GenerationRow rowSelected)
    {
        if (rowSelected == GenerationRow.None)
            return false;

        foreach (var strategy in _playCardStrategies
                     .Where(strategy => strategy.Is(selectedCard)
                                        && strategy.CanPlay(selectedCard, player)))
        {
            RemoveCardFromHand(selectedCard, player.PlayerHand);
            strategy.Execute(selectedCard, player, rowSelected);
            return true;
        }

        return false;
    }

    private static void RemoveCardFromHand(Card selectedCard, PlayerHand hand)
    {
        var domainCard = hand.Cards.First(x => x.Name == selectedCard.Name);
        hand.Cards.Remove(domainCard);
    }
}