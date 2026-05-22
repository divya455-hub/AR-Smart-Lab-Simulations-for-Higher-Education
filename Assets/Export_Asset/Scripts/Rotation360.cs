using UnityEngine;

public class RotateOnSelect : MonoBehaviour
{
    [SerializeField] private Camera cam;                 // drag your Main Camera here
    [SerializeField] private LayerMask selectableLayers; // set to "Machines" in Inspector
    [SerializeField] private float rotationSpeed = 5f;

    private static RotateOnSelect current;  // only one selected at a time
    private Vector2 lastPos;

    void Awake()
    {
        if (!cam) cam = Camera.main; // fallback if you tagged MainCamera
    }

    void Update()
    {
        // Mouse
        if (Input.GetMouseButtonDown(0)) SelectAt(Input.mousePosition);
        if (current == this && Input.GetMouseButton(0)) DragTo(Input.mousePosition);

        // Touch
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) SelectAt(t.position);
            if ((t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) && current == this)
                DragTo(t.position);
        }
    }

    void SelectAt(Vector2 screenPos)
    {
        if (!cam) return;
        Ray ray = cam.ScreenPointToRay(screenPos);

        // if no mask set, use all layers; otherwise use the provided mask
        int mask = selectableLayers.value == 0 ? ~0 : selectableLayers.value;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, mask))
        {
            // works even if collider is on a child
            var target = hit.transform.GetComponentInParent<RotateOnSelect>();
            current = target;                 // select only the clicked machine
            lastPos = screenPos;
        }
        else
        {
            if (current == this) current = null; // click empty space -> deselect
        }
    }

    void DragTo(Vector2 screenPos)
    {
        Vector2 delta = screenPos - lastPos;
        float ry = -delta.x * rotationSpeed * Time.deltaTime; // yaw
        float rx =  delta.y * rotationSpeed * Time.deltaTime; // pitch

        transform.Rotate(Vector3.up, ry, Space.World);
        transform.Rotate(transform.right, rx, Space.World);

        lastPos = screenPos;
    }
}
