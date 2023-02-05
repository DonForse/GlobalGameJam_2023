using System.Collections.Generic;
using System.Linq;
using Cards;
using Game;
using UnityEngine.Events;

public static class ObjectiveService
{
    private static List<ObjectiveCard> PrincipalObjectiveCards = new();
    public static UnityEvent<ObjectiveCard> CardClaimed = new ();
    
    public static void Initialize(ObjectiveDeck deck)
    {
        PrincipalObjectiveCards = deck.Cards.ToList();
    }

    public static List<ObjectiveCard> Get()
    {
        return PrincipalObjectiveCards;
    }

    public static void Claim(string name)
    {
        var card = PrincipalObjectiveCards.First(x => x.Name == name);
        PrincipalObjectiveCards.Remove(card);
        CardClaimed.Invoke(card);
    }
}