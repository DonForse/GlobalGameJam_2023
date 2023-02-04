using System;
using UnityEngine.Events;

namespace Cards.Drag
{
    public static class DraggingService
    {
        public static readonly UnityEvent<Card, Action> OnStartDragging = new();
        public static bool IsDragging => _isDragging;
        private static bool _isDragging = false;

        public static void StartDragging(Card card, Action callback)
        {
            _isDragging = true;
            OnStartDragging.Invoke(card, () =>
            {
                _isDragging = false;
                callback();
            });
        }
    }
}