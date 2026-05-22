using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XYPlot : MonoBehaviour
{
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float xInterval;
    [SerializeField] private int xPadding;

    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private float yInterval;
    [SerializeField] private int yPadding;

    private RectTransform circlePoint;
    private RectTransform templateLabelX;
    private RectTransform templateLabelY;
    private RectTransform DashX;
    private RectTransform DashY;

    private RectTransform graphContainer;
    private float widthMax;
    private float widthMin;
    private float heightMax;
    private float heightMin;

    private Vector2 xMB;
    private Vector2 yMB;
    
    public List<RectTransform> points = new List<RectTransform>();
    private List<RectTransform> lines = new List<RectTransform>();
    private List<RectTransform> userCreated = new List<RectTransform>();

    public void Initialize()
    {
        graphContainer = GetComponent<RectTransform>();
        var sizeDelta = graphContainer.sizeDelta;
        
        widthMin =  xPadding;
        heightMin =  yPadding;
        widthMax = graphContainer.rect.width - widthMin;
        heightMax = graphContainer.rect.height - heightMin;

        xMB = MB(xMin, widthMin, xMax, widthMax);
        yMB = MB(yMin, heightMin, yMax, heightMax);

        circlePoint = graphContainer.Find("circlePoint").GetComponent<RectTransform>();
        templateLabelX = graphContainer.Find("TemplateLabelX").GetComponent<RectTransform>();
        templateLabelY = graphContainer.Find("TemplateLabelY").GetComponent<RectTransform>();
        DashX = graphContainer.Find("DashX").GetComponent<RectTransform>();
        DashY = graphContainer.Find("DashY").GetComponent<RectTransform>();
        
        CreateGraph();
    }
    
    public Vector2 XYToGraph(Vector2 anchoredPosition) => new Vector2(anchoredPosition.x * xMB.x + xMB.y, anchoredPosition.y * yMB.x + yMB.y);

    private Vector2 MB(float x1, float y1, float x2, float y2)
    {
        var m = (y1 - y2) / (x1 - x2);
        var b = y1 - m * x1;
        return new Vector2(m, b);
    }

    public RectTransform CreatePoint(Vector2 xyPosition, Color color, bool userCreate = true)
    {
        RectTransform circle = Instantiate(circlePoint);
        circle.SetParent(graphContainer);

        circle.GetComponent<Image>().color = color;
        circle.anchoredPosition = new Vector2(xyPosition.x * xMB.x + xMB.y, xyPosition.y * yMB.x + yMB.y);
        circle.gameObject.SetActive(true);
        circle.name = xyPosition.ToString();
        if (userCreate) userCreated.Add(circle);
        return circle;
    }

    public void CreateGraph()
    {
        for (float i = xMin; i <= xMax; i += xInterval)
        {
            RectTransform labelX = Instantiate(templateLabelX);
            labelX.SetParent(graphContainer);
            var X = i * xMB.x + xMB.y;
            labelX.anchoredPosition = new Vector2(X, -53f);
            var val = Math.Round(i);
            labelX.GetComponent<Text>().text = val.ToString();
            labelX.gameObject.SetActive(true);

            RectTransform dashX = Instantiate(DashX);
            dashX.SetParent(graphContainer);
            dashX.anchoredPosition = new Vector2(X, 0);
            dashX.gameObject.SetActive(true);
            dashX.sizeDelta = new Vector2(dashX.sizeDelta.x, graphContainer.rect.height);
            if(i == 0) dashX.GetComponent<Image>().color = Color.black;
            else dashX.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            
        }

        for (float i = yMin; i <= yMax; i += yInterval)
        {
            RectTransform labelY = Instantiate(templateLabelY);
            labelY.SetParent(graphContainer);
            var Y = i * yMB.x + yMB.y;
            labelY.anchoredPosition = new Vector2(-11f, Y);
            var val = Math.Round(i);
            labelY.GetComponent<Text>().text = val.ToString();
            labelY.gameObject.SetActive(true);

            RectTransform dashY = Instantiate(DashY);
            dashY.SetParent(graphContainer);
            dashY.anchoredPosition = new Vector2(0, Y);
            dashY.gameObject.SetActive(true);
            dashY.sizeDelta = new Vector2(graphContainer.rect.width, dashY.sizeDelta.y);
            if(i == 0) dashY.GetComponent<Image>().color = Color.black;
            else dashY.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        
        
    }
    public RectTransform CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color, bool userCreate = true)
    {
        dotPositionA = XYToGraph(dotPositionA);
        dotPositionB = XYToGraph(dotPositionB);
        
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 direction = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + direction * distance / 2;
        rectTransform.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction));
        if(userCreate) userCreated.Add(rectTransform);
        return rectTransform;
    }

    public void CreatePolygon(List<Vector2> points, Color lineColor, Color pointColor)
    {
        for (int i = 0; i < points.Count; i++)
        {
            CreateDotConnection(points[i], points[(i + 1) % points.Count], lineColor);
        }
        CreatePoints(points, pointColor);
    }

    public void CreatePoints(List<Vector2> points, Color color)
    {
        foreach (var point in points)
        {
            CreatePoint(point, color);
        }
    }
    

    public void CreateLineSegments(List<Segment> segments, Color color)
    {
        for (int i = 0; i < segments.Count; i++)
        {
            Vector2 v1 = new Vector2(segments[i].Start.x, segments[i].Start.y);
            Vector2 v2 = new Vector2(segments[i].End.x, segments[i].End.y);
            CreateDotConnection(v1, v2, color);
        }
    }
    
    public void CreateNewPoint(Vector2 mousePosition)
    {
        
        if (mousePosition.x >= graphContainer.offsetMin.x && mousePosition.x <= Screen.width + graphContainer.offsetMax.x
        && mousePosition.y >= graphContainer.offsetMin.y && mousePosition.y <= Screen.width + graphContainer.offsetMax.y)
        {

            var v1 = mousePosition - graphContainer.offsetMin;
            v1 = GraphToXY(v1);
            points.Add(CreatePoint(v1, Color.blue, false));
        }
    }

    public Vector2 GraphToXY(Vector2 graph)
    {
        return new Vector2((graph.x - xMB.y) / xMB.x, (graph.y - yMB.y) / yMB.x);
    }

    public void ClearUser()
    {
        foreach (var rectTransform in userCreated)
        {
            Destroy(rectTransform.gameObject);
        }

        userCreated.Clear();
    }

    public void ClearLines()
    {
        foreach (var rectTransform in lines)
        {
            Destroy(rectTransform.gameObject);
        }

        lines.Clear();
    }

    public void CreatePolygon()
    {
        ClearLines();
        for (int i = 0; i < points.Count; i++)
        {
            lines.Add(CreateDotConnection(GraphToXY(points[i].anchoredPosition), GraphToXY(points[(i + 1) % points.Count].anchoredPosition), Color.gray, false));
        }
    }

    public List<Vector2> XYPoints()
    {
        List<Vector2> temp = new List<Vector2>();
        foreach (var point in points)
        {
            temp.Add(GraphToXY(point.anchoredPosition));
        }

        return temp;
    }

    public void CreateSegments()
    {
        ClearLines();
        var temp = XYPoints();
        for (int i = 0; i < points.Count - 1; i+=2)
        {

            lines.Add(CreateDotConnection(temp[i], temp[i + 1], Color.cyan, false));

        }
    }
    public class Segment
{
    public Vector2 Start { get; set; }
    public Vector2 End { get; set; }

    public Segment(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }
}


}
