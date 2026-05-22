using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public Transform source;
    public Transform target;

    void Update()
    {
        if (source == null || target == null) return;

        Vector3 direction = target.position - source.position;
        float distance = direction.magnitude;

        // Position
        transform.position = source.position + direction / 2f;

        // Rotate the cylinder to point from source to target
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(90, 0, 0); // Rotate to align cylinder's Y with direction

        // Scale (X/Z: thin beam, Y: length)
        transform.localScale = new Vector3(0.01f, distance / 2f, 0.01f);
    }
}
