using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs; // Prefabs for H2, O, Ca
    public List<MoleculeReaction> moleculeReactions = new List<MoleculeReaction>(); // List of molecule reactions

    private Dictionary<string, GameObject> trackedObjects = new Dictionary<string, GameObject>(); // Tracks active objects

    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnPrefab(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject obj))
            {
                obj.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    obj.transform.position = trackedImage.transform.position;
                    obj.transform.rotation = trackedImage.transform.rotation;
                }
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (trackedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject obj))
            {
                Destroy(obj);
                trackedObjects.Remove(trackedImage.referenceImage.name);
            }
        }
    }

    private void SpawnPrefab(ARTrackedImage trackedImage)
    {
        foreach (var arPrefab in ArPrefabs)
        {
            if (trackedImage.referenceImage.name == arPrefab.name)
            {
                GameObject newPrefab = Instantiate(arPrefab, trackedImage.transform);
                trackedObjects[trackedImage.referenceImage.name] = newPrefab;

                // Assign correct tag
                if (arPrefab.name == "H2_Bg") newPrefab.tag = "Hydrogen";
                else if (arPrefab.name == "O_Bg") newPrefab.tag = "Oxygen";
                else if (arPrefab.name == "Ca_Bg") newPrefab.tag = "Calcium";

                // Add Collider & Rigidbody
                SphereCollider collider = newPrefab.AddComponent<SphereCollider>();
                collider.isTrigger = true;
                collider.radius = 0.05f;

                Rigidbody rb = newPrefab.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = false;

                // Add Collision Handler Script
                AtomCollider atomCollider = newPrefab.AddComponent<AtomCollider>();
                atomCollider.imageTracker = this;

                Debug.Log("Instantiated prefab: " + arPrefab.name);
            }
        }
    }

    public void HandleMoleculeFormation(GameObject atom1, GameObject atom2, Vector3 spawnPosition)
    {
        foreach (var reaction in moleculeReactions)
        {
            if ((reaction.element1 == atom1.tag && reaction.element2 == atom2.tag) ||
                (reaction.element1 == atom2.tag && reaction.element2 == atom1.tag))
            {
                Debug.Log(atom1.tag + " and " + atom2.tag + " collided! Creating molecule...");
                ReplaceAtomsWithMolecule(atom1, atom2, reaction.resultPrefab, spawnPosition);
                return;
            }
        }
    }

    public void ReplaceAtomsWithMolecule(GameObject atom1, GameObject atom2, GameObject moleculePrefab, Vector3 spawnPosition)
    {
        Destroy(atom1);
        Destroy(atom2);
        GameObject newMolecule = Instantiate(moleculePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Created Molecule: " + moleculePrefab.name);
    }
}

[System.Serializable]
public class MoleculeReaction
{
    public string element1; // First element (e.g., "Hydrogen")
    public string element2; // Second element (e.g., "Oxygen")
    public GameObject resultPrefab; // Resulting molecule prefab (e.g., H₂O)
}