using System.Collections.Generic;
using System.Linq;
using Actions;
using Cards;
using Game;

public class GameBoard
{
    private List<PlayerBoard> playerBoards = new();
    private ObjectiveDeck _principalObjectiveDeck;

    public void Initialize(Player player, Player npc, DiscardCard discardCard, ObjectiveDeck principalObjectivesDeck)
    {
        playerBoards.Add(new PlayerBoard(player, discardCard));
        playerBoards.Add(new PlayerBoard(npc, discardCard));
        _principalObjectiveDeck = principalObjectivesDeck;
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

    public bool HasCardObjective(Player player, ObjectiveCard card)
    {
       var playerBoard = FindPlayerBoardByPlayer(player);
       return playerBoard.First().HasObjectiveCondition(card);
    }

    public void CompleteObjectiveCard(ObjectiveCard card, Player player)
    {
        var playerBoard = FindPlayerBoardByPlayer(player);
        playerBoard.First().RemoveCards(card);
    }

    public void AddPoint(Player player)
    {
        var playerBoard = FindPlayerBoardByPlayer(player).First();
        playerBoard.AddTrophy();
    }

    public bool CanClaimTrophy(string cardId, Player player)
    {
        var playerBoard = FindPlayerBoardByPlayer(player);
        var principalObjectiveCard = _principalObjectiveDeck.Cards.First(x => x.Name == cardId);
        return playerBoard.Any(x => x.HasObjectiveCondition(principalObjectiveCard));
    }

    public void CompletePrincipalObjectiveCard(string cardId, Player player)
    {
        var playerBoard = FindPlayerBoardByPlayer(player);
        var principalObjectiveCard = _principalObjectiveDeck.Cards.First(x => x.Name == cardId);
        playerBoard.First().RemoveCards(principalObjectiveCard);
    }
}