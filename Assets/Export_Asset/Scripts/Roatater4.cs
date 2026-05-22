using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    public float rotationSpeed = 5f; // Drag sensitivity
    private Vector2 lastTouchPosition;

    void Update()
    {
        // For touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - lastTouchPosition;
                float rotateY = -delta.x * rotationSpeed * Time.deltaTime; // Horizontal drag
                float rotateX = delta.y * rotationSpeed * Time.deltaTime;   // Vertical drag

                transform.Rotate(Vector3.up, rotateY, Space.World);
                transform.Rotate(Vector3.right, rotateX, Space.World);

                lastTouchPosition = touch.position;
            }
        }

        // For mouse (editor testing)
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastTouchPosition;
            float rotateY = -delta.x * rotationSpeed * Time.deltaTime;
            float rotateX = delta.y * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotateY, Space.World);
            transform.Rotate(Vector3.right, rotateX, Space.World);

            lastTouchPosition = Input.mousePosition;
        }
    }
}
