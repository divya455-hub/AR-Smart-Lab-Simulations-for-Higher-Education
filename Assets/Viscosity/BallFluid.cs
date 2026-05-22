using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallFluid : MonoBehaviour
{
    Rigidbody rb;

    // smoothing & ambient (when not in fluid)
    public float ambientViscosity = 0f;
    public float viscosityLerpSpeed = 6f;

    float targetViscosity = 0f;
    float currentViscosity = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentViscosity = ambientViscosity;
        targetViscosity = ambientViscosity;
    }

    public void SetViscosity(float v)
    {
        targetViscosity = v;
    }

    public void ResetViscosity()
    {
        targetViscosity = ambientViscosity;
    }

    void FixedUpdate()
    {
        // Smooth transition
        currentViscosity = Mathf.Lerp(currentViscosity, targetViscosity, Time.fixedDeltaTime * viscosityLerpSpeed);

        // Apply viscous force: F = -k * v (we use acceleration mode)
        if (currentViscosity > 0.0001f)
        {
            Vector3 viscousAccel = -currentViscosity * rb.velocity;
            rb.AddForce(viscousAccel, ForceMode.Acceleration);
        }
    }
}
