using UnityEngine;

public class SwipeToExit : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    void Update()
    {
        // Detect the beginning of a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        // Detect the end of a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            DetectSwipe();
        }
    }

    private void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        // Check if the swipe is horizontal or vertical
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
        {
            if (swipeDelta.x > 50) // Swipe right
            {
                Debug.Log("Swipe Right Detected - Show Exit Confirmation");
                ConfirmExit();
            }
        }
        else // Vertical swipe
        {
            if (swipeDelta.y < -50) // Swipe down
            {
                Debug.Log("Swipe Down Detected - Show Exit Confirmation");
                ConfirmExit();
            }
        }
    }

    private void ConfirmExit()
    {
        // Replace with your UI confirmation dialog logic
        bool userConfirmed = true; // Example logic, replace with actual UI confirmation

        if (userConfirmed)
        {
            Debug.Log("Exiting the app...");
            Application.Quit(); // Close the app
        }
        else
        {
            Debug.Log("Exit canceled.");
        }
    }
}
