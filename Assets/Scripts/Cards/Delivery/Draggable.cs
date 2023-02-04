using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private float startXPos;
    private float startYPos;

    private bool isDragging = false;

    private void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    public void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);

        if (!_camera.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = _camera.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos);
        transform.localPosition =
            new Vector3(mousePos.x - startXPos, mousePos.y - startYPos, transform.localPosition.z);
        Debug.Log(transform.localPosition);
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!_camera.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = _camera.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - transform.localPosition.x;
        startYPos = mousePos.y - transform.localPosition.y;

        isDragging = true;
    }
}