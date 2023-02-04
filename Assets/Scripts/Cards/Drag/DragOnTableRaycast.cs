using System;
using System.Collections;
using System.Collections.Generic;
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

        private void HandleDragging(Card card, Action stopDragging)
        {
            StartCoroutine(ListenToRayCast(card, stopDragging));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if(ray != null)
                Gizmos.DrawRay((Ray) ray);
        }

        private IEnumerator ListenToRayCast(Card cardData, Action stopDragging)
        {

            while (DraggingService.IsDragging)
            {
                yield return null;
                // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast((Ray) ray, out var hitInfo))
                {
                    var slot = hitInfo.collider.gameObject.GetComponent<SnappeableSlot>();
                    if (slot == null) continue;
                    
                    
                    slot.OnOvering();

                    if (Input.GetMouseButtonUp(0))
                    {
                        slot.SnapCard(cardData);
                        stopDragging();
                    }
                }
                // if (Input.GetMouseButtonDown(0))
                // {
                //     Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                //     RaycastHit hitInfo;
                //     if (Physics.Raycast(ray, out hitInfo))
                //     {
                //         // Do something with the hitInfo
                //     }
                // }
                
                // Vector3 cameraPos = cameraTransform.position;
                // Vector3 rayDirection = cameraTransform.forward;
                //
                // Ray ray = new Ray(cameraPos, rayDirection);
                // RaycastHit hit;
                //
                // if (Physics.Raycast(ray, out hit))
                // {
                //     var slot = hit.collider.gameObject.GetComponent<SnappeableSlot>();
                //     if (slot == null) continue;
                //
                //     slot.SnapCard(cardData);
                //     stopDragging();
                // }
            }

            ray = null;
        }
    }
}