using System;
using System.Collections.Generic;
using Actions;
using Cards;
using UnityEngine;

public class PlayerBoard
{
    public Player Player;
    public int PlayerPoints;

    private List<Card> ChildRow = new();
    private List<Card> ParentRow= new();
    private List<Card> GrandParentRow= new();
    private readonly DiscardCard _discardCard;

    private static BoardView BoardView() => GameObject.FindObjectOfType<BoardView>();

    public PlayerBoard(Player player, DiscardCard discardCard)
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

    public void RemoveCardFromRow(GenerationRow generation)
    {
        switch (generation)
        {
            case GenerationRow.None:
                break;
            case GenerationRow.Child:
                foreach (var card in ChildRow)
                    _discardCard.Execute(card);
                ChildRow.Clear();
                break;
            case GenerationRow.Parent:

                foreach (var card in ParentRow)
                    _discardCard.Execute(card);
                ParentRow.Clear();
                break;
            case GenerationRow.GrandParent:
                foreach (var card in GrandParentRow)
                    _discardCard.Execute(card);
                GrandParentRow.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(generation), generation, null);
        }
        BoardView().RemoveRow(Player, generation);

    }

    public bool HasObjectiveCondition(ObjectiveCard card)
    {
        return (ChildRow.Count >= card.Children && ParentRow.Count >= card.Parents &&
                GrandParentRow.Count > card.GrandParents);
    }

    public void RemoveCards(ObjectiveCard card)
    {
        for (int i = 0; i < card.Children; i++)
        {
            _discardCard.Execute(ChildRow[0]);
            ChildRow.RemoveAt(0);
            BoardView().Remove(Player, GenerationRow.Child);
        }
        for (int i = 0; i < card.Parents; i++)
        {
            _discardCard.Execute(ParentRow[0]);
            ParentRow.RemoveAt(0);
            BoardView().Remove(Player, GenerationRow.Parent);
        }
        for (int i = 0; i < card.GrandParents; i++)
        {
            _discardCard.Execute(GrandParentRow[0]);
            GrandParentRow.RemoveAt(0);
            BoardView().Remove(Player, GenerationRow.GrandParent);
        }
    }

    public void AddTrophy()
    {
        PlayerPoints += 1;
        BoardView().AddTrophy();
    }
}