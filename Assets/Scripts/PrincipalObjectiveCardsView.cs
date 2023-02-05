using System.Collections.Generic;
using Cards;
using Game;
using UnityEngine;

public class PrincipalObjectiveCardsView : MonoBehaviour
{
    [SerializeField] private ObjectiveCardView objectiveCardViewPrefab;
    [SerializeField] private Transform cardsContainer;
    private readonly List<ObjectiveCardView> _cardViews = new();
    public void AddCards(ObjectiveDeck deck)
    {
        foreach (var card in deck.Cards) AddCard(card);
    }
    
    private void AddCard(ObjectiveCard card)
    {
        var go = Instantiate(objectiveCardViewPrefab, cardsContainer);
        var objectiveValues = new int[] { card.GrandParents, card.Parents, card.Children };
        go.Setup(card.Drawing, card.Name, objectiveValues);
        _cardViews.Add(go);
    }
}