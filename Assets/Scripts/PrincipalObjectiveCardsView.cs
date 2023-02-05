using System.Collections.Generic;
using Actions;
using Cards;
using Game;
using UnityEngine;

public class PrincipalObjectiveCardsView : MonoBehaviour
{
    [SerializeField] private ObjectiveCardView objectiveCardViewPrefab;
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private List<ObjectiveCardView> _cardViews = new();
    private CanClaimTrophy _canClaimTrophy;
    private ClaimTrophy _claimTrophy;

    public void Init(ObjectiveDeck deck, CanClaimTrophy canClaimTrophy, ClaimTrophy claimTrophy)
    {
        _claimTrophy = claimTrophy;
        _canClaimTrophy = canClaimTrophy;
        AddCards(deck);
    }

    private void AddCards(ObjectiveDeck deck)
    {
        foreach (var card in deck.Cards) AddCard(card);
    }

    private void AddCard(ObjectiveCard card)
    {
        var go = Instantiate(objectiveCardViewPrefab, cardsContainer);
        var objectiveValues = new int[] { card.GrandParents, card.Parents, card.Children };
        go.Init(_canClaimTrophy, _claimTrophy);
        go.Setup(card.Drawing, card.Name, objectiveValues);
        _cardViews.Add(go);
    }
}