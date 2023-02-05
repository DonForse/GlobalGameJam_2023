using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PricipalObjectiveCardRepository", menuName = "ScriptableObjects/PricipalObjectiveCardRepository", order = 2)]
public class PrincipalObjectiveCardRepositoryScriptableObject : ScriptableObject
{
    public PrincipalObjectiveCard[] PrincipalOjectiveCards;
    public Sprite CardFrame;
    
    public Card GetFromId(string selectedCardCardId)
    {
        var principalObjectiveCard = PrincipalOjectiveCards.FirstOrDefault(x => x.Name == selectedCardCardId);
        if (principalObjectiveCard != null) return principalObjectiveCard;
        
        return null;
    }
}