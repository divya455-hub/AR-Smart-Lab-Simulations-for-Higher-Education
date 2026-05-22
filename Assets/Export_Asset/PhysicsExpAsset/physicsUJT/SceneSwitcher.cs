using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadGraphScene()
    {
        SceneManager.LoadScene("GraphScene"); // Use the exact name of your graph scene
    }
}
