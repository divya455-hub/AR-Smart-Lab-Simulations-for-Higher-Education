using UnityEngine;

public class AppManager : MonoBehaviour
{
    public GameObject homeUI;   // Your Home Canvas
    public GameObject arRoot;   // XR Origin + ARSession + Managers

    void Start()
    {
        ShowHome(); // Start with Home
    }

    public void ShowHome()
    {
        homeUI.SetActive(true);
        arRoot.SetActive(false);
    }

    public void StartAR()
    {
        homeUI.SetActive(false);
        arRoot.SetActive(true);
    }
}
