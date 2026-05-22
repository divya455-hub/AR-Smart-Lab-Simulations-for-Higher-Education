using UnityEngine;

public class ObjectCollisionHandler : MonoBehaviour
{
    public GameObject resultingPrefab; // The resulting molecule prefab (e.g., H₂O)

    private void OnCollisionEnter(Collision collision)
    {
        // Check if this object is H₂ and the other is O, or vice versa
        if ((collision.gameObject.tag == "H2"))
        {
            // Destroy the individual objects
           // Destroy(gameObject);
           // Destroy(collision.gameObject);
           this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            // Spawn the resulting molecule (H₂O)
            Vector3 spawnPosition = (transform.position + collision.transform.position) / 2;
            Instantiate(resultingPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("H₂O molecule spawned!");
        }
    }
}
