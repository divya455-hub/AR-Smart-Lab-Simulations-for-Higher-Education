using UnityEngine;

public class LEDToggle : MonoBehaviour
{
    public GameObject ledObject; // Assign your LED object in the Inspector

    private bool isOn = false;

    public void ToggleLED()
    {
        isOn = !isOn;
        ledObject.SetActive(isOn);
    }
}
