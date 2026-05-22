using UnityEngine;

public class HomeUIController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject homePanel;     // Canvas panel that acts as Home
    [SerializeField] private GameObject playAudioButton; // Your audio button (optional)

    [Header("AR Root")]
    [SerializeField] private GameObject arRoot;        // Parent object that contains AR Session + AR Session Origin, etc.

    void Start()
    {
        // Show Home, hide AR at launch
        if (homePanel) homePanel.SetActive(true);
        if (playAudioButton) playAudioButton.SetActive(false);
        if (arRoot) arRoot.SetActive(false);
    }

    // Hook this to Start AR button
    public void StartAR()
    {
        if (homePanel) homePanel.SetActive(false);
        if (arRoot) arRoot.SetActive(true);
        if (playAudioButton) playAudioButton.SetActive(true);
    }

    // Optional: add this to a Back button in AR if you create one
    public void BackToHome()
    {
        if (arRoot) arRoot.SetActive(false);
        if (playAudioButton) playAudioButton.SetActive(false);
        if (homePanel) homePanel.SetActive(true);
    }

    // Optional Exit button (works in build, not in editor)
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit pressed (works only in build).");
    }
}
