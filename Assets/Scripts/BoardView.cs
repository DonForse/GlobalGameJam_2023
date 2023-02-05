using UnityEngine;

public class BoardView : MonoBehaviour
{
    public PlayerSideBoardView playerSide;
    public PlayerSideBoardView opponentSide;
    
    public void AddTrophy()
    {
    }

    public void AddCard(Player player, Card card, GenerationRow row)
    {
        if (!player.IsOpponent)
            playerSide.AddCard(card, row);
        else
            opponentSide.AddCard(card, row);
    }

    public void RemoveRow(Player player, GenerationRow row)
    {
        if (!player.IsOpponent)
            playerSide.RemoveRow(row);
        else
            opponentSide.RemoveRow(row);
    }

    public void Remove(Player player, GenerationRow row)
    {
        if (!player.IsOpponent)
            playerSide.RemoveCard(row);
        else
            opponentSide.RemoveCard(row);
    }
}