using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PNJunctionManager : MonoBehaviour
{
    [Header("Slider")]
    public Slider voltageSlider; // Set range: 0V to 1V in Inspector

    [Header("Text Display")]
    public TMP_Text voltageText;
    public TMP_Text currentText;

    [Header("Graph Plotting")]
    public XYPlot xyPlot;

    private float voltage;
    private float current;

    void Update()
    {
        if (voltageSlider == null || voltageText == null || currentText == null)
            return;

        voltage = voltageSlider.value;

        // Forward Bias Current Calculation:
        // Below 0.6V -> 0 current
        // After 0.6V -> Exponential growth
        current = (voltage < 0.6f) ? 0f : Mathf.Pow(10f, (voltage - 0.6f) * 10f);

        // Clamp current to max 20 µA
        current = Mathf.Clamp(current, 0f, 20f);

        // Update UI
        voltageText.text = $"V: {voltage:F2} V";
        currentText.text = $"I: {current:F2} µA";
    }

    public void PlotCurrentPoint()
    {
        if (xyPlot == null)
        {
            Debug.LogWarning("XYPlot not assigned.");
            return;
        }

        // Create a blue point on the graph
        xyPlot.CreatePoint(new Vector2(voltage, current), Color.blue);
        Debug.Log($"🔵 Plotted: ({voltage:F2} V, {current:F2} µA)");
    }
}
