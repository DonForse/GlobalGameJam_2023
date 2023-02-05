using System;
using Actions;

public class Player
{
    public readonly string Id = Guid.NewGuid().ToString();
    public PlayerHand PlayerHand;
}