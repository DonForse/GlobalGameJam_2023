using System.Collections.Generic;

public class GameBoard
{
    private List<PlayerBoard> playerBoards = new();

    public void Initialize(Player player, Player npc)
    {
        playerBoards.Add(new PlayerBoard(player));
        playerBoards.Add(new PlayerBoard(npc));
    }

    public void AddCard(Card card, Player player, GenerationRow row)
    {
        foreach (var playerBoard in playerBoards)
        {
            if (playerBoard.Player != player) continue;

            playerBoard.Add(card, row);
        }
    }
}