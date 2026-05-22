// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class UJTManager : MonoBehaviour
// {
//     public Slider veSlider;
//     public Slider vb2b1Slider;

//     public TMP_Text veText;
//     public TMP_Text ieText;

//     public GraphPlotter plotter;

//     void Update()
//     {
//         if (veSlider == null || vb2b1Slider == null || veText == null || ieText == null || plotter == null)
//             return;

//         float ve = veSlider.value;
//         float vb = vb2b1Slider.value;

//         float ie = (ve < 0.7f) ? 0 : Mathf.Clamp((vb - ve) * 5f, 0, 20f);

//         veText.text = $"V_E: {ve:F2}V";
//         ieText.text = $"I_E: {ie:F2}mA";
//     }

//     public void PlotPoint()
//     {
//         float ve = veSlider.value;
//         float vb = vb2b1Slider.value;
//         float ie = (ve < 0.7f) ? 0 : Mathf.Clamp((vb - ve) * 5f, 0, 20f);

//         plotter.PlotPoint(ve, ie);
//     }
// }


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UJTManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider veSlider;
    public Slider vb2b1Slider;

    [Header("Text Displays")]
    public TMP_Text veText;
    public TMP_Text ieText;

    [Header("Graph Plotting")]
    public XYPlot xyPlot; // Assign this in Inspector

    private float currentVE;
    private float currentIE;

    void Update()
    {
        if (veSlider == null || vb2b1Slider == null || veText == null || ieText == null)
            return;

        currentVE = veSlider.value;
        float vb = vb2b1Slider.value;

        // Calculate IE based on UJT characteristic
        currentIE = (currentVE < 0.7f) ? 0 : Mathf.Clamp((vb - currentVE) * 5f, 0, 20f);

        // Update UI Text
        veText.text = $"V_E: {currentVE:F2} V";
        ieText.text = $"I_E: {currentIE:F2} mA";
    }

    // 🟢 Call this from a UI Button to plot the current point
    public void PlotCurrentPoint()
    {
        if (xyPlot == null)
        {
            Debug.LogWarning("XYPlot is not assigned in UJTManager.");
            return;
        }

        xyPlot.CreatePoint(new Vector2(currentVE, currentIE), Color.blue);
    }
}
