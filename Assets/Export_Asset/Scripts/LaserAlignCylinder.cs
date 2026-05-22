using UnityEngine;

public class LaserAlignCylinder : MonoBehaviour
{
    public Transform source; // LED Cube
    public Transform target; // Screen

    void Update()
    {
        Vector3 direction = target.position - source.position;
        float distance = direction.magnitude;

        transform.position = source.position + direction / 2;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.localScale = new Vector3(0.02f, 0.02f, distance);
    }
}
