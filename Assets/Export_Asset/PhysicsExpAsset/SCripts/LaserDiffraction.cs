using UnityEngine;

public class LaserDiffraction : MonoBehaviour
{
    public Transform grating;  // The object where diffraction occurs
    public Transform screen;   // The screen where beams hit

    private LineRenderer[] lasers;  // Array for multiple beams
    private int beamCount = 4;      // Number of diffraction beams

    void Start()
    {
        // Create multiple LineRenderers (for each beam)
        lasers = new LineRenderer[beamCount];

        for (int i = 0; i < beamCount; i++)
        {
            GameObject laserObj = new GameObject("LaserBeam" + i);
            lasers[i] = laserObj.AddComponent<LineRenderer>();
            lasers[i].startWidth = 0.02f;
            lasers[i].endWidth = 0.02f;
            lasers[i].material = new Material(Shader.Find("Sprites/Default"));
            lasers[i].material.color = Color.red;
            lasers[i].positionCount = 3;
        }
    }

    void Update()
    {
        SimulateLaser();
    }

    void SimulateLaser()
    {
        Vector3 laserStart = transform.position;
        Vector3 gratingPosition = grating.position;

        // Define diffraction angles (adjust for better spread)
        float theta1 = 10f;  // Left beam
        float theta2 = -10f;
        float theta3 = 0f;// Right beam

        // Compute beam directions
        Vector3 forward = (screen.position - grating.position).normalized;
        Vector3 direction0 = forward;  // Zero-order (straight)
        Vector3 direction1 = Quaternion.Euler(0, theta1, 0) * forward; // Left diffraction
        Vector3 direction2 = Quaternion.Euler(0, theta2, 0) * forward;
        Vector3 direction3 = Quaternion.Euler(0, theta3, 0) * forward;// Right diffraction

        // Set positions for each beam
        lasers[0].SetPosition(0, laserStart);
        lasers[0].SetPosition(1, gratingPosition);
        lasers[1].SetPosition(0, gratingPosition);
        lasers[1].SetPosition(1, gratingPosition + direction1 * 3f);
        lasers[2].SetPosition(0, gratingPosition);
        lasers[2].SetPosition(1, gratingPosition + direction2 * 3f);
        lasers[3].SetPosition(0, gratingPosition);
        lasers[3].SetPosition(1, gratingPosition + direction3 * 3f);
    }
}
