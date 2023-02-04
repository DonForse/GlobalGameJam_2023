using System;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private OverlayCardView overlayCardViewPrefab;
    [SerializeField] private Transform cardsContainer;
    private Action<OverlayCardView> _onDragOverlayCardStart;
    private Action<OverlayCardView> _onDragOverlayCardEnd;


    public HandView WithOnCardSelected(Action<OverlayCardView> onDragStart, Action<OverlayCardView> onDragEnd)
    {
        _onDragOverlayCardStart = onDragStart;
        _onDragOverlayCardEnd = onDragStart;

        return this;
    }

    public void AddCard(Card card)
    {
        var go = Instantiate(overlayCardViewPrefab, cardsContainer);
        go = go.WithDragging(OnOverlayCardStartDrag, OnOverlayCardEndDrag);
        go.Setup(card.Drawing, card.Name);
    }

    private void OnOverlayCardStartDrag(OverlayCardView selectedCard)
    {
        _onDragOverlayCardStart.Invoke(selectedCard);
        var pos = this.cardsContainer.transform.position;
        this.cardsContainer.transform.position = new Vector3(pos.x, pos.y - 1000, pos.z);
    }
    
    private void OnOverlayCardEndDrag(OverlayCardView selectedCard)
    {
        _onDragOverlayCardEnd.Invoke(selectedCard);
        var pos = this.cardsContainer.transform.position;
        this.cardsContainer.transform.position = new Vector3(pos.x, pos.y + 1000, pos.z);
    }
}