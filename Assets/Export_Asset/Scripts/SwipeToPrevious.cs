using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SwipeToPrevious : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public string previousSceneName; // Name of the previous scene (swipe right)

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

        // Check for a horizontal swipe to the right
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) && swipeDelta.x > 50) // Swipe right
        {
            Debug.Log("Swipe Right Detected - Navigate to Previous Scene");
            NavigateToPrevious();
        }
    }

    private void NavigateToPrevious()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName); // Load the previous scene
        }
        else
        {
            Debug.LogWarning("Previous scene name is not set!");
        }
    }
}
