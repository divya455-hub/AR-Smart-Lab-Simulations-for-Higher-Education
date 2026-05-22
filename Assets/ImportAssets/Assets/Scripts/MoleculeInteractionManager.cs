using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MoleculeInteractionManager : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public GameObject hydrogenPrefab;  // Prefab for H2
    public GameObject oxygenPrefab;   // Prefab for O
    public GameObject waterPrefab;    // Prefab for H2O (resultant molecule)

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Handle newly detected images
        foreach (var trackedImage in args.added)
        {
            SpawnObject(trackedImage);
        }

        // Update objects based on tracked image changes
        foreach (var trackedImage in args.updated)
        {
            UpdateObjectPosition(trackedImage);
        }

        // Remove objects when the image is no longer tracked
        foreach (var trackedImage in args.removed)
        {
            if (spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                Destroy(spawnedObjects[trackedImage.referenceImage.name]);
                spawnedObjects.Remove(trackedImage.referenceImage.name);
            }
        }
    }

    private void SpawnObject(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        GameObject prefabToSpawn = null;
        if (imageName == "H2") prefabToSpawn = hydrogenPrefab;
        else if (imageName == "O") prefabToSpawn = oxygenPrefab;

        if (prefabToSpawn != null && !spawnedObjects.ContainsKey(imageName))
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedObject.name = imageName;
            spawnedObjects[imageName] = spawnedObject;

            // Add Rigidbody and Collider for interaction
            Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
            rb.useGravity = false; // Gravity is disabled for AR
            spawnedObject.AddComponent<BoxCollider>();
        }
    }

    private void UpdateObjectPosition(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject obj = spawnedObjects[trackedImage.referenceImage.name];
            obj.transform.position = trackedImage.transform.position;
            obj.transform.rotation = trackedImage.transform.rotation;
        }
    }
}
