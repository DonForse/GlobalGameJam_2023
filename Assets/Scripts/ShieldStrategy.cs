using Cards;
using UnityEngine;

public class DeckObjectiveStrategy : IPlayCardStrategy
{
    private readonly GameBoard _gameBoard;

    public DeckObjectiveStrategy(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public bool Is(Card card) => card.GetType() == typeof(DeckObjectiveCard);

    public bool CanPlay(Card card, Player player)
    {
        var objectiveCard = (DeckObjectiveCard)card;
        return _gameBoard.HasCardObjective(player, objectiveCard);
        // if (objectiveCard.GrandParents)
    }

    public void Execute(Card card, Player player, GenerationRow row)
    {
    }
}