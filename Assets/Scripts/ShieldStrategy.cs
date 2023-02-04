using Cards;
using UnityEngine;

public class ShieldStrategy : IPlayCardStrategy
{
    private readonly GameBoard _gameBoard;

    public ShieldStrategy(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public bool Is(Card card) => card.GetType() == typeof(ShieldCard);

    public bool CanPlay(Card card, Player player)
    {
        return false;
        // if (objectiveCard.GrandParents)
    }

    public void Execute(Card card, Player player, GenerationRow row)
    {
    }
}