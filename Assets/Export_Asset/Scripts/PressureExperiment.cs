/*using UnityEngine;
using UnityEngine.UI;

public class PressureExperiment : MonoBehaviour
{
    public Transform piston; // Round 3D model on top of the cylinder
    public Slider pressureSlider; // UI slider
    public Text pressureText; // UI text to display pressure
    public Renderer cylinderRenderer; // Transparent cylinder material
    public ParticleSystem airParticles; // Optional: Air particles inside cylinder

    public float minY = 0.5f; // Minimum Y position (lowest compression)
    public float maxY = 2.0f; // Maximum Y position (highest volume)
    public float initialPressure = 1.0f; // Initial atmospheric pressure (1 atm)

    private float initialVolume; // Initial volume when the slider is at minimum
    private Color lowPressureColor = Color.blue;
    private Color highPressureColor = Color.red;

    void Start()
    {
        initialVolume = minY - maxY;
        pressureSlider.onValueChanged.AddListener(UpdatePressure);
        UpdatePressure(pressureSlider.value);
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch detected: " + Input.GetTouch(0).position);
        }
    }


    void UpdatePressure(float value)
    {
        // Move the piston based on slider value
        float newY = Mathf.Lerp(maxY, minY, value);
        piston.position = new Vector3(piston.position.x, newY, piston.position.z);

        // Calculate new volume
        float newVolume = newY - minY;

        // Apply Boyle's Law: P1 * V1 = P2 * V2
        float newPressure = (initialPressure * initialVolume) / newVolume;

        // Update UI
        pressureText.text = $"Pressure: {newPressure:F2} atm";

        // Change cylinder color based on pressure
        float colorLerp = Mathf.InverseLerp(1.0f, 5.0f, newPressure); // Normalize pressure range
        cylinderRenderer.material.color = Color.Lerp(lowPressureColor, highPressureColor, colorLerp);

        // Adjust particle density (if airParticles is assigned)
        if (airParticles != null)
        {
            var emission = airParticles.emission;
            emission.rateOverTime = Mathf.Lerp(10, 100, colorLerp); // More particles at high pressure
        }
    }
}*/
using UnityEngine;
using UnityEngine.UI;

public class PressureExperiment : MonoBehaviour
{
    public Transform piston; // Round 3D model on top of the cylinder
    public Slider pressureSlider; // UI slider
    public Text pressureText; // UI text to display pressure
    public Renderer cylinderRenderer; // Transparent cylinder material
    public ParticleSystem airParticles; // Optional: Air particles inside cylinder

    public float minY = 0.5f; // Minimum Y position (lowest compression)
    public float maxY = 2.0f; // Maximum Y position (highest volume)
    public float initialPressure = 1.0f; // Initial atmospheric pressure (1 atm)

    private float initialVolume; // Initial volume when the slider is at minimum
    private Color lowPressureColor = Color.blue;
    private Color highPressureColor = Color.red;

    void Start()
    {
        initialVolume = maxY - minY;
        pressureSlider.onValueChanged.AddListener(UpdatePressure);
        UpdatePressure(pressureSlider.value);
    }

    void UpdatePressure(float value)
    {
        // Move the piston based on slider value
        float newY = Mathf.Lerp(maxY, minY, value);
        piston.position = new Vector3(piston.position.x, newY, piston.position.z);

        // Calculate new volume
        float newVolume = newY - minY;

        // Apply Boyle's Law: P1 * V1 = P2 * V2
        float newPressure = (initialPressure * initialVolume) / newVolume;

        // Update UI
        pressureText.text = $"Pressure: {newPressure:F2} atm";

        // Change cylinder color based on pressure
        float colorLerp = Mathf.InverseLerp(1.0f, 5.0f, newPressure); // Normalize pressure range
        cylinderRenderer.material.color = Color.Lerp(lowPressureColor, highPressureColor, colorLerp);

        // Adjust particle density (if airParticles is assigned)
        if (airParticles != null)
        {
            var emission = airParticles.emission;
            emission.rateOverTime = Mathf.Lerp(10, 100, colorLerp); // More particles at high pressure
        }
    }
}