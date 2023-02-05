using System;
using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private OverlayCardView overlayCardViewPrefab;
    [SerializeField] private Transform cardsContainer;
    private Action<OverlayCardView> _onDragOverlayCardStart;
    private Action<OverlayCardView> _onDragOverlayCardEnd;

    private List<OverlayCardView> _cardViews = new();
    private TurnService _turnService;

    public HandView WithOnCardSelected(Action<OverlayCardView> onDragStart, Action<OverlayCardView> onDragEnd)
    {
        _onDragOverlayCardStart = onDragStart;
        _onDragOverlayCardEnd = onDragEnd;
        return this;
    }

    public HandView WithTurnService(TurnService turnService)
    {
        _turnService = turnService;
        return this;
    }

    public void AddCard(Card card)
    {
        var go = Instantiate(overlayCardViewPrefab, cardsContainer);
        go = go.WithDragging(OnOverlayCardStartDrag, OnOverlayCardEndDrag);
        go.Setup(card.Drawing, card.Name);
        _cardViews.Add(go);
    }

    private void OnOverlayCardStartDrag(OverlayCardView selectedCard)
    {
        if (_turnService.GetTurn() != PlayerEnum.Player)
            return;
        var pos = this.cardsContainer.transform.position;
        this.cardsContainer.transform.position = new Vector3(pos.x, pos.y - 1000, pos.z);
        _onDragOverlayCardStart.Invoke(selectedCard);
    }

    private void OnOverlayCardEndDrag(OverlayCardView selectedCard)
    {
        if (_turnService.GetTurn() != PlayerEnum.Player)
            return;
        var pos = this.cardsContainer.transform.position;
        this.cardsContainer.transform.position = new Vector3(pos.x, pos.y + 1000, pos.z);
        _onDragOverlayCardEnd.Invoke(selectedCard);

    }

    public void RemoveCard(Card selectedCard)
    {
        OverlayCardView cardToRemove = null; 
        foreach (var card in _cardViews)
        {
            if (card.CardId != selectedCard.Name) continue;
            
            cardToRemove = card;
            break;
        }

        _cardViews.Remove(cardToRemove);
        Destroy(cardToRemove.gameObject);
    }
}