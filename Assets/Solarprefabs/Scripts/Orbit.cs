using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform sun;   // drag and drop Sun object here
    public float orbitSpeed = 10f; // set different speed for each planet in Inspector

    void Update()
    {
        if (sun != null)
        {
            // rotate around Sun's position
            transform.RotateAround(sun.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
