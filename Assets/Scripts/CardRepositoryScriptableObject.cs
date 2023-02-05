using System.Linq;
using Cards;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRepository", menuName = "ScriptableObjects/CardRepository", order = 1)]
public class CardRepositoryScriptableObject : ScriptableObject
{
    public FamilyMemberCard[] FamilyMemberCards;
    public SabotageCard[] SabotageCards;
    public ShieldCard[] ShieldCards;
    public DeckObjectiveCard[] DeckObjectiveCard;
    public Sprite CardFrame;

    public Card GetFromId(string selectedCardCardId)
    {
        var familyMemberCard = FamilyMemberCards.FirstOrDefault(x => x.Name == selectedCardCardId);
        if (familyMemberCard != null) return familyMemberCard;
        
        var sabotageCard = SabotageCards.FirstOrDefault(x => x.Name == selectedCardCardId);
        if (sabotageCard != null) return sabotageCard;
        
        var shieldCard = ShieldCards.FirstOrDefault(x => x.Name == selectedCardCardId);
        if (shieldCard != null) return shieldCard;

        var deckObjectiveCard = DeckObjectiveCard.FirstOrDefault(x => x.Name == selectedCardCardId);
        if (deckObjectiveCard != null) return deckObjectiveCard;
        
        return null;
    }
}