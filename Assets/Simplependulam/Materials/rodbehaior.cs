using UnityEngine;

public class RodVisuals : MonoBehaviour
{
    // The fixed pivot point (Hanger position). Must match the pivot point used in PendulumController.
    private readonly Vector3 pivotPoint = new Vector3(0f, 3f, 0f);

    void Update()
    {
        // Safety check to ensure the script has a parent (the Bob)
        if (transform.parent == null) return;

        // Get the Bob's current position
        Vector3 bobPosition = transform.parent.position;

        // --- 1. Calculate Length and Position (Stretching the Rod) ---

        // Calculate the distance between the pivot and the bob
        float distance = Vector3.Distance(bobPosition, pivotPoint);

        // Stretch the Rod (Cylinder) along its Y-axis by half the distance
        // (Unity Cylinders are scaled from the center, so scale Y by distance / 2)
        transform.localScale = new Vector3(0.1f, distance / 2f, 0.1f);

        // Move the Rod to the center point between the pivot and the bob
        transform.position = Vector3.Lerp(pivotPoint, bobPosition, 0.5f);

        // --- 2. Correct the Rotation (Fixing the Top Point) ---

        // Calculate the direction vector pointing from the pivot to the bob
        Vector3 direction = bobPosition - pivotPoint;

        // Calculate the signed angle of this vector relative to the vertical (Vector3.down) 
        // We use the Z-axis as the axis of rotation for the angle calculation.
        float angle = Vector3.SignedAngle(Vector3.down, direction, Vector3.forward);

        // Apply this rotation only around the Z-axis, which correctly simulates the swing 
        // while keeping the rotation point centered at the pivot point visually.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}