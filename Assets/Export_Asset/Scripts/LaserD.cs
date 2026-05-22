using UnityEngine;

public class LaserD : MonoBehaviour
{
    public float dragSpeed = 0.1f;
    public Vector2 xMovementLimit = new Vector2(-1f, 1f);  // Change this to suit your range
    private Camera mainCam;
    private Vector3 offset;
    private BoxCollider boxCollider;

    public Transform ringImage;  // Assign your ring image here
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float minX = -1f;
    public float maxX = 1f;

    void Start()
    {
        mainCam = Camera.main;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        HandleMouseDrag();

        // Update ring scale based on X position
        if (ringImage != null)
        {
            float t = Mathf.InverseLerp(minX, maxX, transform.position.x);
            float scale = Mathf.Lerp(minScale, maxScale, t);
            ringImage.localScale = new Vector3(scale, scale, scale);
        }
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                offset = transform.position - GetMouseWorldPosition();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition() + offset;
            float clampedX = Mathf.Clamp(mouseWorldPos.x, xMovementLimit.x, xMovementLimit.y);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
        return mainCam.ScreenToWorldPoint(screenMousePos);
    }

    public void EnableDragging()
    {
        boxCollider.enabled = true;
    }

    public void DisableDragging()
    {
        boxCollider.enabled = false;
    }
}
