using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class PlayerBoard
{
    public Player Player;
    public int PlayerPoints;
    
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

        BoardView().AddCard(Player, card, row);
    }

    private static BoardView BoardView() => GameObject.FindObjectOfType<BoardView>();

    public void RemoveCardFromRow(GenerationRow generation)
    {
        switch (generation)
        {
            case GenerationRow.None:
                break;
            case GenerationRow.Child:
                ChildRow.Clear();
                break;
            case GenerationRow.Parent:
                ParentRow.Clear();
                break;
            case GenerationRow.GrandParent:
                GrandParentRow.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(generation), generation, null);
        }
        BoardView().RemoveRow(Player, generation);

    }

    public bool HasObjectiveCondition(ObjectiveCard card)
    {
        return (ChildRow.Count >= card.Childs && ParentRow.Count >= card.Parents &&
                GrandParentRow.Count > card.GrandParents);
    }

    public void RemoveCards(ObjectiveCard card)
    {
        for (int i = 0; i < card.Childs; i++)
        {
            ChildRow.RemoveAt(0);
        }
        for (int i = 0; i < card.Parents; i++)
        {
            ParentRow.RemoveAt(0);
        }
        for (int i = 0; i < card.GrandParents; i++)
        {
            GrandParentRow.RemoveAt(0);
        }
    }
}