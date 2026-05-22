using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;

    void Update()
    {
        // Check for touch input in Android
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.transform == transform)
                            {
                                isDragging = true;
                                zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
                                offset = transform.position - GetWorldPosition(touch.position);
                            }
                        }
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            transform.position = GetWorldPosition(touch.position) + offset;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isDragging = false;
                        break;
                }
            }
        }
        else
        {
            // For testing in the Unity editor (mouse input)
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
                        offset = transform.position - GetWorldPosition(Input.mousePosition);
                    }
                }
            }
            else if (Input.GetMouseButton(0) && isDragging)
            {
                transform.position = GetWorldPosition(Input.mousePosition) + offset;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }

    // Get the world position based on the screen touch or mouse position
    private Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        screenPosition.z = zCoord; // Use the z-axis depth value of the object
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}
