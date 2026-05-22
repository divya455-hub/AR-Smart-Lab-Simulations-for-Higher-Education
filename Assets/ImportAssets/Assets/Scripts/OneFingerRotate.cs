using UnityEngine;

/// <summary>
/// This script allows an object to be rotated using one-finger gestures on Android devices.
/// Attach this script to the object you want to rotate.
/// </summary>
public class OneFingerRotate : MonoBehaviour
{
    // The speed at which the object will rotate
    public float rotationSpeed = 0.2f;

    // Track the initial touch position
    private Vector2 lastTouchPosition;

    // Flag to check if the user is dragging (rotating)
    private bool isRotating = false;

    // Store the currently rotating object
    private static OneFingerRotate currentRotatingObject;

    void Update()
    {
        // Check if the user has touched the screen
        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            Touch touch = Input.GetTouch(0);

            // Perform a raycast to check what object is being touched
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // On touch start, check if the ray hits this object
                    if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                    {
                        // This object is being touched; store the touch position and set the rotating flag to true
                        lastTouchPosition = touch.position;
                        isRotating = true;
                        currentRotatingObject = this; // Set this object as the currently rotating object
                    }
                    break;

                case TouchPhase.Moved:
                    if (isRotating && currentRotatingObject == this)
                    {
                        // Calculate the difference between the current and last touch position
                        Vector2 deltaTouch = touch.position - lastTouchPosition;

                        // Rotate the object based on horizontal finger movement
                        float rotationAngle = deltaTouch.x * rotationSpeed;

                        // Apply the rotation to the object around the Y-axis
                        transform.Rotate(Vector3.up, -rotationAngle, Space.World);

                        // Update the last touch position
                        lastTouchPosition = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // When the touch ends or is canceled, stop the rotation
                    if (currentRotatingObject == this)
                    {
                        isRotating = false;
                        currentRotatingObject = null; // Clear the currently rotating object
                    }
                    break;
            }
        }
        else
        {
            // If there's no touch, reset the currently rotating object
            currentRotatingObject = null;
        }
    }
}