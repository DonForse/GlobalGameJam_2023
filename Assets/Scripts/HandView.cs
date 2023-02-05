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
        _turnService.OnTurnChange.AddListener(ToggleHand);
        return this;
    }

    private void ToggleHand(PlayerEnum turn)
    {
        if (PlayerEnum.Npc == turn)
            Hide();
        else
            Show();
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
        Hide();
        _onDragOverlayCardStart.Invoke(selectedCard);
    }

    private void OnOverlayCardEndDrag(OverlayCardView selectedCard)
    {
        if (_turnService.GetTurn() != PlayerEnum.Player)
            return;
        Show();
        _onDragOverlayCardEnd.Invoke(selectedCard);
    }


    public void Show()
    {
        this.cardsContainer.gameObject.SetActive(true);
    }

    private void Hide()
    { 
        this.cardsContainer.gameObject.SetActive(false);
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