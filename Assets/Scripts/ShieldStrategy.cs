using Actions;
using Cards;
using UnityEngine;

public class ShieldStrategy : IPlayCardStrategy
{
    private readonly DiscardCard _discardCard;

    public ShieldStrategy(DiscardCard discardCard)
    {
        _discardCard = discardCard;
    }

    public bool Is(Card card) => card.GetType() == typeof(ShieldCard);

    public bool CanPlay(Card card, Player player)
    {
        return EstaEnSabotageService.Get();
    }

    public void Execute(Card card, Player player, GenerationRow row)
    {
        _discardCard.Execute(card);
        Debug.Log("shield card completed");
    }
}

public static class EstaEnSabotageService
{
    public static bool Sabotage = false; 
    public static bool Get()
    {
        return Sabotage;
    }
}