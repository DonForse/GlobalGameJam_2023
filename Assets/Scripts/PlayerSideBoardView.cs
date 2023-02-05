using System;
using System.Collections.Generic;
using Cards.Drag;
using UnityEngine;

public class PlayerSideBoardView : MonoBehaviour
{
    public List<GameObject> 
        grandparentsSlots,
        parentsSlots,
        childsSlots;
    public void AddCard(Card card, GenerationRow row)
    {
        Debug.Log($"Add card {card.Name} on row {row.ToString()}");
    }

    public void RemoveRow(GenerationRow row)
    {
        Debug.Log($"Remove row {row.ToString()}");
        
    }
}