using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PNDiodeGraphManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public RectTransform graphContainer;
    public GameObject rowPrefab;
    public Transform tableContent;
    public GameObject dotPrefab;

    public Slider voltageSlider;
    public TextMeshProUGUI voltageText;
    public TextMeshProUGUI currentText;

    public float graphWidth = 500f;
    public float graphHeight = 300f;

    private List<Vector2> plotPoints = new List<Vector2>();

    void Start()
    {
        voltageSlider.minValue = 0f;
        voltageSlider.maxValue = 1f;
        voltageSlider.onValueChanged.AddListener(UpdateVoltageDisplay);
        UpdateVoltageDisplay(voltageSlider.value);
        lineRenderer.positionCount = 0;
    }

    void UpdateVoltageDisplay(float value)
    {
        float current = CalculateCurrent(value);
        voltageText.text = $"Vf : {value:0.00} V";
        currentText.text = $"If : {current:0.00} µA";
    }

    float CalculateCurrent(float voltage)
    {
        if (voltage < 0.25f)
            return 0f;
        else
            return Mathf.Pow(10f, (voltage - 0.6f) * 10f); // Simulated curve
    }

    public void OnPlotPoint()
    {
        float voltage = voltageSlider.value;
        float current = CalculateCurrent(voltage);
        Vector2 point = new Vector2(voltage, current);

        plotPoints.Add(point);
        UpdateGraph();
        AddTableRow(voltage, current);
        CreateDot(point);
    }

    void UpdateGraph()
    {
        lineRenderer.positionCount = plotPoints.Count;
        for (int i = 0; i < plotPoints.Count; i++)
        {
            Vector2 point = plotPoints[i];
            float x = (point.x / 1f) * graphWidth;
            float y = (point.y / 100f) * graphHeight;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    void CreateDot(Vector2 point)
    {
        float x = (point.x / 1f) * graphWidth;
        float y = (point.y / 100f) * graphHeight;

        GameObject dot = Instantiate(dotPrefab, graphContainer);
        dot.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
    }

    void AddTableRow(float voltage, float current)
    {
        GameObject row = Instantiate(rowPrefab, tableContent);
        TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = $"{voltage:0.00} V";
        texts[1].text = $"{current:0.00} µA";
    }
}
