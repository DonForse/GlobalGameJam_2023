using System;
using TMPro;
using UnityEngine;

namespace Cards.Drag
{
    public class SnappeableSlot : MonoBehaviour
    {
        public SpriteRenderer drawRenderer;
        public SpriteRenderer frameRenderer;
        public TMP_Text name;
        public GenerationRow Generation;
        public CardRepositoryScriptableObject cardsRepository;
        
        [Space, Header("Hover")]
        public Renderer hoveringRenderer;
        public Color hoveringColor = Color.yellow;


        public bool IsFree { get; private set; } = true;

        private void Awake()
        {
            Clear();
            HideHovering();
        }
        
        public void ShowCard(Card card)
        {
            IsFree = false;
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

        public void Clear()
        {
            IsFree = true;
            drawRenderer.sprite = null;
            frameRenderer.sprite = null;
            name.text = string.Empty;
        }
    }
}