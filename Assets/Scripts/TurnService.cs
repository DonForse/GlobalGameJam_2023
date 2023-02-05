using UnityEngine.Events;

public class TurnService
{
    private readonly GameView _gameView;
    private readonly PlayCard _playCard;
    private int turn = 0;
    private PlayerEnum playerTurn = PlayerEnum.Player;
    private int _cardsPlayed = 0;
    private bool process = true;

    public TurnService(GameView gameView, PlayCard playCard)
    {
        _gameView = gameView;
        _playCard = playCard;

        _playCard.OnCardPlayed.AddListener(AddCardPlayed);
        TrophiesService.SomeoneWon.AddListener(Stop);
    }

    private void Stop(PlayerEnum arg0)
    {
        process = false;
    }

    public UnityEvent<PlayerEnum> OnTurnChange = new UnityEvent<PlayerEnum>();
    private void AddCardPlayed()
    {
        if (!process) return;
        _cardsPlayed++;
        if (_cardsPlayed >= 2)
        {
            ChangeTurn();
        }
    }

    private void ChangeTurn()
    {
        
        if (!process) return;
        var newPlayer = playerTurn == PlayerEnum.Player ? PlayerEnum.Npc : PlayerEnum.Player;
        playerTurn = newPlayer;
        StartTurn(newPlayer);
        OnTurnChange.Invoke(newPlayer);
    }

    public void StartTurn(PlayerEnum player)
    {
        playerTurn = player;
        _gameView.ShowTurn(player);
        turn++;
        _cardsPlayed = 0;
    }

    public PlayerEnum GetTurn() => playerTurn;
    
}