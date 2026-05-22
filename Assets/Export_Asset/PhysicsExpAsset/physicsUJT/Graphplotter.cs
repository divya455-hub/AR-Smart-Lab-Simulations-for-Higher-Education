using UnityEngine;
using UnityEngine.UI;

public class GraphPlotter : MonoBehaviour
{
    public RectTransform graphArea; // UI Panel
    public GameObject dotPrefab;

    public float maxVE = 5f;  // max X-axis (VE)
    public float maxIE = 30f; // max Y-axis (IE)

    public void PlotPoint(float ve, float ie)
    {
        // Clamp values to avoid going out of bounds
        ve = Mathf.Clamp(ve, 0, maxVE);
        ie = Mathf.Clamp(ie, 0, maxIE);

        float width = graphArea.rect.width;
        float height = graphArea.rect.height;

        float x = (ve / maxVE) * width;
        float y = (ie / maxIE) * height;

        GameObject dot = Instantiate(dotPrefab, graphArea);
        RectTransform rt = dot.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
    }
}
