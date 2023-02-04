using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Drag
{
    public class DragOnTableRaycast : MonoBehaviour
    {
        private void OnEnable()
        {
            DraggingService.OnStartDragging.AddListener(HandleDragging);
        }

        private void HandleDragging(Card card, Action stopDragging)
        {
            StartCoroutine(ListenToRayCast(card, stopDragging));
        }

        private IEnumerator ListenToRayCast(Card cardData, Action stopDragging)
        {
            var camera = Camera.main;
            Transform cameraTransform = camera.transform;

            while (DraggingService.IsDragging)
            {
                Vector3 cameraPos = cameraTransform.position;
                Vector3 rayDirection = cameraTransform.forward;
                
                Ray ray = new Ray(cameraPos, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    var slot = hit.collider.gameObject.GetComponent<SnappeableSlot>();
                    if (slot == null) continue;

                    slot.SnapCard(cardData);
                    stopDragging();
                }
                yield return null;
            }
        }
    }
}