using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageInfo1 : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager m_TrackedImageManager;

    [SerializeField]
    private GameObject modelForCa; // H₂ prefab
    [SerializeField]
    private GameObject modelForO;  // O prefab
    [SerializeField]
    private GameObject modelForCaO;   // H₂O prefab

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;
    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            SpawnModelForImage(newImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            if (spawnedPrefabs.ContainsKey(removedImage.referenceImage.name))
            {
                Destroy(spawnedPrefabs[removedImage.referenceImage.name]);
                spawnedPrefabs.Remove(removedImage.referenceImage.name);
            }
        }
    }

    private void SpawnModelForImage(ARTrackedImage trackedImage)
    {
        GameObject modelToSpawn = null;

        if (trackedImage.referenceImage.name == "Ca")
        {
            modelToSpawn = Instantiate(modelForCa, trackedImage.transform.position, Quaternion.identity);
        }
        else if (trackedImage.referenceImage.name == "O")
        {
            modelToSpawn = Instantiate(modelForO, trackedImage.transform.position, Quaternion.identity);
        }

        if (modelToSpawn != null)
        {
            //   AddCollisionComponents(modelToSpawn);

            // Store spawned object
            spawnedPrefabs[trackedImage.referenceImage.name] = modelToSpawn;
        }
    }

    private void AddCollisionComponents(GameObject obj)
    {
        if (!obj.GetComponent<Collider>())
        {
            obj.AddComponent<SphereCollider>();
        }

        if (!obj.GetComponent<Rigidbody>())
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = false; // Enable collision events
        }

        // Attach the collision handler script
        if (!obj.GetComponent<ObjectCollisionHandler>())
        {
            var collisionHandler = obj.AddComponent<ObjectCollisionHandler>();
            collisionHandler.resultingPrefab = modelForCaO;
        }
    }
}
