using UnityEngine;

[CreateAssetMenu(fileName = "CardRepository", menuName = "ScriptableObjects/CardRepository", order = 1)]
public class CardRepositoryScriptableObject : ScriptableObject
{
    public FamilyMemberCard[] FamilyMemberCards;
    public SabotageCard[] SabotageCards;
    public ShieldCard[] ShieldCards;
    public ObjectiveCard[] ObjectiveCards;
}