using System;
using System.Collections.Generic;
using System.Linq;
using Actions;

public class PlayCard
{
    public event EventHandler OnCardPlayed;
    private readonly List<IPlayCardStrategy> _playCardStrategies;


    public PlayCard(GameBoard gameBoard,
        DiscardCard discardCard,
        Action<Action<bool>> isShieldUsed)
    {
        _playCardStrategies = new List<IPlayCardStrategy>
        {
            new FamilyMemberStrategy(gameBoard),
            new SabotageStrategy(gameBoard, discardCard, isShieldUsed),
            new ShieldStrategy(discardCard)
        };
    }

    public void Execute(Card selectedCard, 
        Player player,
        GenerationRow rowSelected)
    {
        if (rowSelected == GenerationRow.None)
            return;

        RemoveCardFromHand(selectedCard, player.PlayerHand);
        foreach (var strategy in _playCardStrategies
                     .Where(strategy => strategy.Is(selectedCard)
                                        && strategy.CanPlay(selectedCard, player)))
        {
            strategy.Execute(selectedCard, player, rowSelected);
            OnCardPlayed?.Invoke(null, null);
        }
    }

    private static void RemoveCardFromHand(Card selectedCard, PlayerHand hand)
    {
        var domainCard = hand.Cards.First(x => x.Name == selectedCard.Name);
        hand.Cards.Remove(domainCard);
    }
}