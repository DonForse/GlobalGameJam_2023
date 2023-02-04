using System.Collections.Generic;
using System.Linq;
using Actions;

public class PlayCard
{
    private GameBoard _gameBoard;
    private readonly List<IPlayCardStrategy> _playCardStrategies;

    public PlayCard(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
        _playCardStrategies = new List<IPlayCardStrategy>
        {
            new FamilyMemberStrategy()
        };
    }

    public void Execute(Card selectedCard,Player player, PlayerHand hand, GenerationRow rowSelected)
    {
        if (rowSelected == GenerationRow.None)
            return;

        RemoveCardFromHand(selectedCard, hand);

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