using UnityEngine;

public class AtomCollider : MonoBehaviour
{
    public ImageTracker imageTracker;

    private void OnTriggerEnter(Collider other)
    {
        if (imageTracker != null)
        {
            Vector3 spawnPosition = (transform.position + other.transform.position) / 2;
            imageTracker.HandleMoleculeFormation(gameObject, other.gameObject, spawnPosition);
        }
    }
}