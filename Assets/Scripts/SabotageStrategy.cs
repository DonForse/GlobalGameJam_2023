using Actions;
using UnityEngine;

public class SabotageStrategy : IPlayCardStrategy
{
    private readonly GameBoard _gameBoard;
    private readonly DiscardCard _discardCard;

    public SabotageStrategy(GameBoard gameBoard, DiscardCard discardCard)
    {
        _gameBoard = gameBoard;
        _discardCard = discardCard;
    }

    public bool Is(Card card) => card.GetType() == typeof(SabotageCard);
    public bool CanPlay(Card card, Player player) => true;

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _gameBoard.RemoveAllCardsFromOpponent(player, row);
        _discardCard.Execute(card);
        Debug.Log("Sabotage completed");
    }
}