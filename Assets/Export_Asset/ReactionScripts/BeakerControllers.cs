using UnityEngine;

public class BeakerControllers : MonoBehaviour
{
    public GameObject water; // Assign water object
    public Color lowMercuryColor = Color.blue;
    public Color highMercuryColor = Color.red;
    public float mercuryThreshold = 5f; // Change color if mercury level exceeds this
    public GameObject explosionEffect; // Assign explosion effect

    private float currentMercuryLevel = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mercury"))
        {
            currentMercuryLevel += other.transform.localScale.x; // Add mercury size

            if (currentMercuryLevel < mercuryThreshold)
            {
                water.GetComponent<Renderer>().material.color = Color.Lerp(lowMercuryColor, highMercuryColor, currentMercuryLevel / mercuryThreshold);
            }
            else
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(water); // Simulating explosion
            }

            Destroy(other.gameObject); // Remove Mercury prefab after adding
        }
    }
}
