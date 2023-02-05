using System;
using UnityEngine;

namespace Actions
{
    public class LookAroundBehaviour : MonoBehaviour
    {
        public Transform CameraTransform;
        private Quaternion originalRotation;
        [Range(0f, 1f)] public float LerpT = 0.01f;
        
        public float mouseSensitivity = 100f;
        private float xRotation = 0f;


        private void Awake()
        {
            originalRotation = CameraTransform.rotation;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.X))
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.parent.Rotate(Vector3.up * mouseX);
            }
            else
            {
                CameraTransform.rotation = Quaternion.Lerp(CameraTransform.rotation, originalRotation, LerpT);

            }

        }
    }
}