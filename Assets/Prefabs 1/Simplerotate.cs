using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Vector3 rotationSpeed; // Rotation speed in degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
