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
        
        [Space, Header("Hover")]
        public Renderer hoveringRenderer;
        public Color hoveringColor = Color.yellow;

        private void Awake()
        {
            drawRenderer.sprite = null;
            frameRenderer.sprite = null;
            name.text = string.Empty;
            HideHovering();
        }

        public void SnapCard(Card cardData)
        {
            ShowCard(cardData);
        }

        private void ShowCard(Card card)
        {
            HideHovering();
            drawRenderer.sprite = card.Drawing;
            frameRenderer.sprite = cardsRepository.CardFrame;
            name.text = card.Name;
        }

        public void OnOvering() => ShowOvering();

        private void ShowOvering()
        {
            hoveringRenderer.enabled = true;
            hoveringRenderer.material.color = hoveringColor;
        }

        private void HideHovering()
        {
            hoveringRenderer.enabled = false;
        }

        private void Update()
        {
            HideHovering();
        }
    }
}