using System;
using TMPro;
using UnityEngine;

namespace Cards.Drag
{
    public class CardViewSprite : MonoBehaviour
    {
        public SpriteRenderer drawRenderer;
        public TMP_Text name;

        public bool IsFree { get; private set; } = true;

        private void Awake()
        {
            Clear();
        }
        
        public void ShowCard(Card card)
        {
            IsFree = false;
            drawRenderer.sprite = card.Drawing;
            name.text = card.Name;
        }
        
        public void Clear()
        {
            IsFree = true;
            drawRenderer.sprite = null;
            name.text = string.Empty;
        }
    }
}