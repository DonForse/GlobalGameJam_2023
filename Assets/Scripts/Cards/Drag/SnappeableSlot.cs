using UnityEngine;

namespace Cards.Drag
{
    internal class SnappeableSlot : MonoBehaviour
    {
        public SpriteRenderer drawRenderer;
        public SpriteRenderer frameRenderer;
        public CardRepositoryScriptableObject cardsRepository;

        public void SnapCard(Card cardData)
        {
            ShowCard(cardData);
        }

        private void ShowCard(Card card)
        {
            drawRenderer.sprite = card.Drawing;
            frameRenderer.sprite = cardsRepository.CardFrame;
        }
    }
}