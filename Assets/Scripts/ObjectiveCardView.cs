using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectiveCardView: MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text cardName;

    public string CardId;
    public void Setup(Sprite sprite, string name)
    {
        cardImage.sprite = sprite;
        // this.cardName.text = name;
        this.name = name;
        CardId = name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click on thropy card");
    }
}