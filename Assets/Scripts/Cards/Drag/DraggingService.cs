using System;
using UnityEngine.Events;

namespace Cards.Drag
{
    public static class DraggingService
    {
        public static UnityEvent<Card, Action> OnStartDragging = new();
        public static UnityEvent<Card> OnStopDragging = new();
        public static bool IsDragging => _isDragging;
        private static bool _isDragging = true;

    }
}