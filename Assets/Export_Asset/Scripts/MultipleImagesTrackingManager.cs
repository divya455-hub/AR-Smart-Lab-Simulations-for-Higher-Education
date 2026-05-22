using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject[] placedPrefabs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (var prefab in placedPrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name.ToLower(), newPrefab); // store lowercase
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateSpawnedObject(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateSpawnedObject(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name.ToLower(), out GameObject prefab))
            {
                prefab.SetActive(false); // hide if image lost
            }
        }
    }

    private void UpdateSpawnedObject(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name.ToLower();
        Debug.Log("Scanned Image: " + imageName);

        if (spawnedPrefabs.TryGetValue(imageName, out GameObject prefab))
        {
            prefab.SetActive(true);
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
        }
    }
}
