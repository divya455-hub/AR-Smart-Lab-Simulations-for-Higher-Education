using UnityEngine;
using UnityEngine.UI;

public class AcidBaseReaction : MonoBehaviour
{
    public Slider acidBaseSlider;  // Slider to control acid-base ratio
    public Renderer beakerRenderer; // Beaker material to change color
    public Text reactionText;  // UI text to display pH level

    // Define colors for different pH levels
    private Color acidColor = Color.red;      // Acidic solution (pH < 4)
    private Color neutralColor = Color.green; // Neutral solution (pH ~7)
    private Color baseColor = Color.blue;     // Basic solution (pH > 10)

    void Start()
    {
        // Add event listener for slider value changes
        acidBaseSlider.onValueChanged.AddListener(UpdateBeakerColor);
        UpdateBeakerColor(acidBaseSlider.value);
    }
    void UpdateBeakerColor(float value)
    {
        // Simulate pH scale (pH 1 for pure acid, pH 14 for pure base)
        float pH = Mathf.Lerp(1, 14, value);

        // Determine beaker color based on pH range
        Color currentColor;

        if (pH < 4)  // Acidic (Red)
        {
            currentColor = acidColor;
        }
        else if (pH >= 4 && pH <= 10) // Transitioning from Acid to Base
        {
            currentColor = Color.Lerp(acidColor, neutralColor, (pH - 4) / 6); // Interpolates to Green
        }
        else  // Basic (Blue)
        {
            currentColor = Color.Lerp(neutralColor, baseColor, (pH - 10) / 4); // Interpolates to Blue
        }

        // Apply the color to the beaker
        beakerRenderer.material.color = currentColor;

        // Update the UI text to display pH value
        reactionText.text = $"pH Rate: {pH:F1}";
    }

}
