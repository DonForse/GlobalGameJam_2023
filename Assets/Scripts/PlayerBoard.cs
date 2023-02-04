using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoard
{
    public Player Player;

    private List<Card> ChildRow = new();
    private List<Card> ParentRow= new();
    private List<Card> GrandParentRow= new();

    public PlayerBoard(Player player)
    {
        Player = player;
    }

    public void Add(Card card, GenerationRow row)
    {
        Debug.Log($"Add Card to {row}");
        switch (row)
        {
            case GenerationRow.None:
                break;
            case GenerationRow.Child:
                ChildRow.Add(card);
                break;
            case GenerationRow.Parent:
                ParentRow.Add(card);
                break;
            case GenerationRow.GrandParent:
                GrandParentRow.Add(card);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(row), row, null);
        }
    }
}