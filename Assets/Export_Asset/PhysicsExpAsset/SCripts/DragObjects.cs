using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;
    private Camera mainCamera;  // Store the main camera reference

    void Start()
    {
        mainCamera = Camera.main;  // Assign the main camera at start
        if (mainCamera == null)
        {
            Debug.LogError("No Main Camera found in the scene!");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;  // Stop execution if no camera is found

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    zCoord = mainCamera.WorldToScreenPoint(transform.position).z;
                    offset = transform.position - GetWorldPosition(touch.position);
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                transform.position = GetWorldPosition(touch.position) + offset;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        if (mainCamera == null) return Vector3.zero;  // Prevent errors if camera is null

        Vector3 screenPoint = new Vector3(screenPosition.x, screenPosition.y, zCoord);
        return mainCamera.ScreenToWorldPoint(screenPoint);
    }
}
