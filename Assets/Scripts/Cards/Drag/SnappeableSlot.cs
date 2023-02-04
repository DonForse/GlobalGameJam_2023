using System;
using TMPro;
using UnityEngine;

namespace Cards.Drag
{
    internal class SnappeableSlot : MonoBehaviour
    {
        public SpriteRenderer drawRenderer;
        public SpriteRenderer frameRenderer;
        public TMP_Text name;
        public CardRepositoryScriptableObject cardsRepository;

        private void Awake()
        {
            drawRenderer.sprite = null;
            frameRenderer.sprite = null;
            name.text = string.Empty;
        }

        public void SnapCard(Card cardData)
        {
            ShowCard(cardData);
        }

        private void ShowCard(Card card)
        {
            drawRenderer.sprite = card.Drawing;
            frameRenderer.sprite = cardsRepository.CardFrame;
            name.text = card.Name;
        }
    }
}