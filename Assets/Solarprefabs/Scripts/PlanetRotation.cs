using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Adjust per planet

    void Update()
    {
        // Rotate around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
