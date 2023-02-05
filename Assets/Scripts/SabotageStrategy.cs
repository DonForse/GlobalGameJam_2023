using System;
using Actions;
using UnityEngine;

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
    public bool CanPlay(Card card, Player player) => true;

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _isShieldUsed(result =>
        {
            if (!result)
                _gameBoard.RemoveAllCardsFromOpponent(player, row);
        });
        _discardCard.Execute(card);
        Debug.Log("Sabotage completed");
    }
}