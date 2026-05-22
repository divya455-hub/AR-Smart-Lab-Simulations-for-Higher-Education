using UnityEngine;
using UnityEngine.UI;

public class ChemicalReaction : MonoBehaviour
{
    public Slider reactionSlider; // UI Slider
    public Text concentrationText; // UI Text for concentration
    public Renderer glassRenderer; // Cylinder (beaker) material
    public ParticleSystem bubblingParticles; // Bubbles inside the beaker

    private Color startColor = Color.blue; // Low concentration
    private Color endColor = Color.red;   // High concentration

    void Start()
    {
        reactionSlider.onValueChanged.AddListener(UpdateReaction);
        UpdateReaction(reactionSlider.value);
    }

    void UpdateReaction(float value)
    {
        // Update the concentration text
        concentrationText.text = $"Concentration: {value:F2} M";

        // Change glass color based on reaction value
        glassRenderer.material.color = Color.Lerp(startColor, endColor, value);

        // Adjust particle emission (bubble intensity)
        if (bubblingParticles != null)
        {
            var emission = bubblingParticles.emission;
            emission.rateOverTime = Mathf.Lerp(10, 100, value); // More bubbles at higher concentration
        }
    }
}
