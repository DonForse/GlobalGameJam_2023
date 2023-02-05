using System;
using UnityEngine;

namespace Cards.Drag
{
    internal class GenerationRowDragTarget : MonoBehaviour
    {
        public GenerationRow Generation;
        public Renderer ThisRenderer;

        public void OnHovering()
        {
            ThisRenderer.enabled = true;
        }

        private void Update()
        {
            ThisRenderer.enabled = false;
        }
    }
}