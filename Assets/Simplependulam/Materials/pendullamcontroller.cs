using UnityEngine;

public class PendulumController : MonoBehaviour
{
    // --- SETTINGS (Adjustable in Inspector) ---
    public float length = 5.0f;           // Length of the pendulum 'string'
    public float gravity = 9.81f;         // Acceleration due to gravity
    public float initialAngle = 60.0f;    // Starting angle (in degrees). Use a high value like 60 for better motion.
    public float damping = 0.03f;         // Damping factor (0.03 is a good starting point)

    // The pivot point (Hanger position). Assuming (0, 3, 0)
    private readonly Vector3 pivotPoint = new Vector3(0f, 3f, 0f);

    // --- PHYSICS STATE (MADE PUBLIC FOR GAMEMANAGER TO READ) ---
    public float currentAngle;            // Current angle (in RADIANS)
    public float angularVelocity = 0f;    // Current speed of the swing

    // --- PRIVATE UTILITY VARIABLES ---
    private float angularFrequency;       // omega (w)
    // Removed timeElapsed and startRotation as they are not needed here.

    // Called when the script starts
    void Start()
    {
        // Calculate the angular frequency (omega)
        angularFrequency = Mathf.Sqrt(gravity / length);

        // Reset the pendulum to its starting state immediately
        ResetPendulum();
    }

    // Called every frame (The Physics Engine)
    void Update()
    {
        // --- 1. Calculate Angular Acceleration ---
        // Formula: alpha = -(g/L) * sin(theta). This is the restoring force.
        float angularAcceleration = -(gravity / length) * Mathf.Sin(currentAngle);

        // --- 2. Update Angular Velocity (applying Damping) ---
        angularVelocity += angularAcceleration * Time.deltaTime;
        // The damping term reduces the speed each frame
        angularVelocity *= (1.0f - damping * Time.deltaTime);

        // --- 3. Update Angle ---
        currentAngle += angularVelocity * Time.deltaTime;

        // --- 4. Update the Bob's Position ---
        // Calculate the Bob's position relative to the pivot
        float x = Mathf.Sin(currentAngle) * length;
        float y = -Mathf.Cos(currentAngle) * length;

        // The Bob's final position is (PivotX + x, PivotY + y, 0)
        transform.position = pivotPoint + new Vector3(x, y, 0);
    }

    // This is called by the GameManager when you click START
    public void ResetPendulum()
    {
        // Reset physics state
        angularVelocity = 0f;

        // Set the angle back to the start (converted to radians)
        currentAngle = initialAngle * Mathf.Deg2Rad;

        // Recalculate and set the visual position
        float x = Mathf.Sin(currentAngle) * length;
        float y = -Mathf.Cos(currentAngle) * length;

        // Place the Bob at the starting position
        transform.position = pivotPoint + new Vector3(x, y, 0);
    }
}