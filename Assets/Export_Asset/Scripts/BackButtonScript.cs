using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    public string previousSceneName; // Set this in the Inspector

    public void GoBack()
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
