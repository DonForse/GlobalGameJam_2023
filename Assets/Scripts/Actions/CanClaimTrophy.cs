namespace Actions
{
    public class CanClaimTrophy
    {
        private GameBoard _gameBoard;
        private readonly GetPlayerFromTurn _getPlayerFromTurn;

        public CanClaimTrophy(GameBoard gameBoard, GetPlayerFromTurn getPlayerFromTurn)
        {
            _gameBoard = gameBoard;
            _getPlayerFromTurn = getPlayerFromTurn;
        }

        public bool Execute(string cardId)
        {
            var player = _getPlayerFromTurn.Execute();
            return _gameBoard.CanClaimTrophy(cardId, player);
        }
    }
}