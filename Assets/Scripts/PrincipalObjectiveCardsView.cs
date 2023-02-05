using System.Collections.Generic;
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
    
    private void AddCard(Card card)
    {
        var go = Instantiate(objectiveCardViewPrefab, cardsContainer);
        go.Setup(card.Drawing, card.Name);
        _cardViews.Add(go);
    }
}