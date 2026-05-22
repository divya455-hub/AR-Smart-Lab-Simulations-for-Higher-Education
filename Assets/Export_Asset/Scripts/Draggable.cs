using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        // Handle Touch Input (for Mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = GetTouchWorldPos(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    isDragging = true;
                    offset = transform.position - touchPos;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                transform.position = touchPos + offset;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }

        // Handle Mouse Input (for PC)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - GetMouseWorldPos();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private Vector3 GetTouchWorldPos(Vector2 touchPosition)
    {
        Vector3 touchPoint = new Vector3(touchPosition.x, touchPosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        return Camera.main.ScreenToWorldPoint(touchPoint);
    }
}
