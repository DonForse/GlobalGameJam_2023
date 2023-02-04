using System.Collections.Generic;
using System.Linq;

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
        foreach (var playerBoard in FindPlayerBoardByPlayer(player))
            playerBoard.Add(card, row);
    }

    private IEnumerable<PlayerBoard> FindPlayerBoardByPlayer(Player player) => 
        playerBoards.Where(playerBoard => playerBoard.Player == player);

    public void RemoveAllCardsFromOpponent(Player player, GenerationRow row)
    {
        foreach (var playerBoard in FindGameBoardFromOpponentPlayer(player))
            playerBoard.RemoveCardFromRow(row);
    }

    private IEnumerable<PlayerBoard> FindGameBoardFromOpponentPlayer(Player player) => 
        playerBoards.Where(playerBoard => playerBoard.Player != player);
}