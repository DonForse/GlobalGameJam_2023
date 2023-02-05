using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OverlayCardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text cardName;
    private Action<OverlayCardView> _startDrag;
    private Action<OverlayCardView> _endDrag;

    public string CardId;
    public void Setup(Sprite sprite, string name)
    {
        cardImage.sprite = sprite;
        cardName.text = name;
        this.name = name;
        CardId = name;
    }

    public OverlayCardView WithDragging(Action<OverlayCardView> startDrag, Action<OverlayCardView> endDrag)
    {
        _startDrag = startDrag;
        _endDrag = endDrag;
        return this;
    }

    public void OnPointerUp(PointerEventData eventData) => _endDrag.Invoke(this);

    public void OnPointerDown(PointerEventData eventData) => _startDrag.Invoke(this);
}