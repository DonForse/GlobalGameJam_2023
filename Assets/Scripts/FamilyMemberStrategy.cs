using UnityEngine.Events;

public class FamilyMemberStrategy: IPlayCardStrategy
{
    private readonly GameBoard _gameBoard;
    private UnityEvent _cardPlayed = new();

    public FamilyMemberStrategy(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public bool Is(Card card) => card.GetType() == typeof(FamilyMemberCard);
    public bool CanPlay(Card card, Player player) => true;

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _gameBoard.AddCard(card,player, row);
        _cardPlayed.Invoke();
    }

    public UnityEvent CardPlayed => _cardPlayed;
}