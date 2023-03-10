using System;
using System.Collections.Generic;
using System.Linq;
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
        var slot = GetFreeSlot(row);
        slot.ShowCard(card);
    }

    private CardViewSprite GetFreeSlot(GenerationRow generationRow) =>
        GetSnappeableSlots(generationRow)
            .First(it => it.IsFree);

    private IEnumerable<CardViewSprite> GetSnappeableSlots(GenerationRow generationRow) =>
        GetSlotsFor(generationRow)
            .Select(it => it.GetComponentInChildren<CardViewSprite>());

    private List<GameObject> GetSlotsFor(GenerationRow generationRow)
    {
        return generationRow switch
        {
            GenerationRow.Child => childsSlots,
            GenerationRow.Parent => parentsSlots,
            GenerationRow.GrandParent => grandparentsSlots,
            _ => throw new NotImplementedException($"Tried to play on {generationRow.ToString()}")
        };
    }

    public void RemoveRow(GenerationRow row)
    {
        Debug.Log($"Remove row {row.ToString()}");
        foreach (var slot in GetSnappeableSlots(row))
        {
            slot.Clear();
        }
    }

    public void RemoveCard(GenerationRow row)
    {
        Debug.Log($"Remove card {row.ToString()}");
        GetSnappeableSlots(row).Last(it => !it.IsFree).Clear();
    }
}