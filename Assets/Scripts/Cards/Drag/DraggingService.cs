using System;
using UnityEngine.Events;

namespace Cards.Drag
{
    public static class DraggingService
    {
        public static readonly UnityEvent<Card, Action<GenerationRow>> OnStartDragging = new();
        public static bool IsDragging => _isDragging;
        private static bool _isDragging = false;

        public static void StartDragging(Card card, Action<GenerationRow> callback)
        {
            _isDragging = true;
            OnStartDragging.Invoke(card, generationRow =>
            {
                _isDragging = false;
                callback(generationRow);
            });
        }
    }
}