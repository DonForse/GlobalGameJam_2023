using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text name;
    public void Setup(Sprite sprite, string name)
    {
        cardImage.sprite = sprite;
    }
}