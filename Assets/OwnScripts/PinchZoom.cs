using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float zoomSpeed = 0.01f;    // Zoom sensitivity
    public float minScale = 0.5f;      // Minimum zoom size
    public float maxScale = 3f;        // Maximum zoom size
    public float doubleTapTime = 0.3f; // Max time between taps for double tap

    private float lastTapTime = 0f;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    void Start()
    {
        // Save original size and rotation for reset
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // --- Double Tap Reset ---
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    ResetTransform();
                }
                lastTapTime = Time.time;
            }
        }

        // --- Pinch Zoom ---
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Find previous and current distance between touches
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            // Apply scaling with limits
            float newScale = Mathf.Clamp(transform.localScale.x + difference * zoomSpeed, minScale, maxScale);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    void ResetTransform()
    {
        transform.localScale = originalScale;
        transform.rotation = originalRotation;
    }
}
