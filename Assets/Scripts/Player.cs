using Actions;

public class Player
{
    public PlayerHand PlayerHand;
    public readonly bool IsOpponent;

    public Player(bool isOpponent)
    {
        IsOpponent = isOpponent;
    }
}