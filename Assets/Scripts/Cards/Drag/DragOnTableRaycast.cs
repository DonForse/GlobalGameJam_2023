using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards.Drag
{
    public class DragOnTableRaycast : MonoBehaviour
    {
        private Ray? ray = null;
        public Camera mainCamera;
        private void OnEnable()
        {
            DraggingService.OnStartDragging.AddListener(HandleDragging);
        }

        private void HandleDragging(Card card, Action<GenerationRow> stopDragging)
        {
            StartCoroutine(ListenToRayCast(card, stopDragging));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if(ray != null)
                Gizmos.DrawRay((Ray) ray);
        }

        private IEnumerator ListenToRayCast(Card cardData, Action<GenerationRow> stopDragging)
        {

            while (DraggingService.IsDragging)
            {
                yield return null;
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll((Ray) ray);

                var slot = hits.Select(it => it.collider.GetComponent<SnappeableSlot>())
                    .FirstOrDefault(it => it != null);
                if (slot == null) continue;
                
                
                OnOveringSlot(cardData, stopDragging, slot);

            }

            ray = null;
        }

        private static void OnOveringSlot(Card cardData, Action<GenerationRow> stopDragging, SnappeableSlot slot)
        {
            slot.OnOvering();

            if (Input.GetMouseButtonUp(0))
            {
                slot.SnapCard(cardData);
                stopDragging(slot.Generation);
            }
        }
    }
}