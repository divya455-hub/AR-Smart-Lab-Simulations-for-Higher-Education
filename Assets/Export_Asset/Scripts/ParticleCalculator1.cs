using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticleCalculator1 : MonoBehaviour
{
    public TMP_InputField inputD;       // For D (in 10^-2 m)
    public TMP_InputField input2r;      // For 2r (in 10^-2 m)
    public TMP_Text resultText;

    private float wavelength = 632.8e-9f; // Diode laser ? in meters

    public void CalculateRadius()
    {
        // Parse inputs
        if (float.TryParse(inputD.text, out float Dvalue) &&
            float.TryParse(input2r.text, out float diameter))
        {
            float r = (diameter / 2) * 0.01f;  // Convert cm to m
            float D = Dvalue * 0.01f;          // Convert cm to m

            float a = (1.22f * wavelength * D) / (2 * r);  // Final calculation

            resultText.text = $"Radius of microparticle (a): {a:E2} m";
        }
        else
        {
            resultText.text = "Invalid input. Please enter numeric values.";
        }
    }
}
