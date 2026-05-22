using UnityEngine;
using UnityEngine.UI;

public class ARMeasurement : MonoBehaviour
{
    public Transform laserSource;      // Laser source position
    public Transform ringOrScreen;     // Main screen or ring position
    public Transform leftEdgePoint;    // For 2r measurement (left spot)
    public Transform rightEdgePoint;   // For 2r measurement (right spot)

    public Text laserDistanceText;
    public Text twoRText;              // Text to display 2r
    public LineRenderer measurementLine;

    void Update()
    {
        if (laserSource == null || ringOrScreen == null) return;

        // Distance from laser to screen
        float laserDistance = Vector3.Distance(laserSource.position, ringOrScreen.position);
        laserDistanceText.text = $"Laser Distance: {laserDistance * 100f:F1} m";

        // Line from laser to screen
        if (measurementLine != null)
        {
            measurementLine.positionCount = 2;
            measurementLine.SetPosition(0, laserSource.position);
            measurementLine.SetPosition(1, ringOrScreen.position);
        }

        // 2r measurement between left and right points
        if (leftEdgePoint != null && rightEdgePoint != null)
        {
            float twoR = Vector3.Distance(leftEdgePoint.position, rightEdgePoint.position);
            twoRText.text = $"2r: {twoR * 100f:F1} m";  // Display in cm
        }
    }
}
