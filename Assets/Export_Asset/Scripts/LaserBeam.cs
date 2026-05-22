using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    public Transform grating;
    public Transform screen;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 3; // Laser Source ? Grating ? Screen
    }

    void Update()
    {
        if (lr.positionCount < 3) return;  // Safety check

        lr.SetPosition(0, transform.position);  // Laser Source
        lr.SetPosition(1, grating.position);    // Glass Plate / Grating
        lr.SetPosition(2, screen.position);     // Screen
    }
}
