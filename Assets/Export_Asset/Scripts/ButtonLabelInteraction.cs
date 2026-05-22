using UnityEngine;
using TMPro;
using UnityEngine.UI; // Required for Button

public class ButtonLabelInteraction : MonoBehaviour
{
    public TextMeshProUGUI labelText; // Reference to TextMeshProUGUI for the label
    public Button yourButton; // Reference to the button

    private void Start()
    {
        // Ensure the label is initially hidden
        if (labelText != null)
        {
            labelText.alpha = 0f; // Hide the label initially
        }

        // Add listener to the button to call the ShowLabel method when clicked
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(ShowLabel);
        }
    }

    // Method to show the label when the button is clicked
    private void ShowLabel()
    {
        if (labelText != null)
        {
            // Set the text to visible (you can add custom behavior to update text if needed)
            labelText.alpha = 1f;
        }
    }
}