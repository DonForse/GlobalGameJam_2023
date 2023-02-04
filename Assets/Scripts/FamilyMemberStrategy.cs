public class FamilyMemberStrategy: IPlayCardStrategy
{
    private GameBoard _gameBoard;

    public FamilyMemberStrategy(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public bool Is(Card card) => card.GetType() == typeof(FamilyMemberCard);

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _gameBoard.AddCard(card,player, row);
    }
}