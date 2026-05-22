using UnityEngine;
using UnityEngine.UI;

public class XYPlot1 : MonoBehaviour
{
    public RectTransform graphArea;     // Assign your UI Panel here in Inspector
    public GameObject dotPrefab;        // Assign a small UI Image prefab (e.g., a circle or square)

    public float xMin = 0f;
    public float xMax = 1f;
    public float yMin = 0f;
    public float yMax = 20f;

    public void CreatePoint(Vector2 point, Color color)
    {
        if (dotPrefab == null || graphArea == null)
        {
            Debug.LogWarning("dotPrefab or graphArea not assigned.");
            return;
        }

        float graphWidth = graphArea.rect.width;
        float graphHeight = graphArea.rect.height;

        // Normalize coordinates to fit inside the graph
        float xPos = Mathf.InverseLerp(xMin, xMax, point.x) * graphWidth;
        float yPos = Mathf.InverseLerp(yMin, yMax, point.y) * graphHeight;

        // Instantiate the dot at correct location inside the graphArea
        GameObject dot = Instantiate(dotPrefab, graphArea);
        RectTransform rt = dot.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(xPos, yPos);

        // Set color of the point
        Image img = dot.GetComponent<Image>();
        if (img != null)
            img.color = color;
    }
}
