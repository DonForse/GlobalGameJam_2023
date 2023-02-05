using Cards;
using UnityEngine.Events;

public class DeckObjectiveStrategy : IPlayCardStrategy
{
    private readonly GameBoard _gameBoard;
    private UnityEvent _cardPlayed = new();

    public DeckObjectiveStrategy(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public bool Is(Card card) => card.GetType() == typeof(DeckObjectiveCard);

    public bool CanPlay(Card card, Player player)
    {
        var objectiveCard = (ObjectiveCard)card;
        return _gameBoard.HasCardObjective(player, objectiveCard);
    }

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _gameBoard.CompleteObjectiveCard((ObjectiveCard)card, player);
        _gameBoard.AddPoint(player);
        TrophiesService.AddTrophy(player);
    }

    public UnityEvent CardPlayed => _cardPlayed;
}