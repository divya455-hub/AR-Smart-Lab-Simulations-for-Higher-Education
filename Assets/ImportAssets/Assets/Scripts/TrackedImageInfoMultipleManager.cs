using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private Button dismissButton;
    [SerializeField] private Text imageTrackedText;
    [SerializeField] private GameObject[] arObjectsToPlace;
    [SerializeField] private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    private ARTrackedImageManager m_TrackedImageManager;
    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        // Setup dictionary with AR objects
        foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            newARObject.SetActive(false); // Start inactive
            arObjects.Add(arObject.name, newARObject);
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Dismiss() => welcomePanel.SetActive(false);

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("OnTrackedImagesChanged triggered.");

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            Debug.Log($"Added image: {trackedImage.referenceImage.name}");
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            Debug.Log($"Updated image: {trackedImage.referenceImage.name}");
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Debug.Log($"Removed image: {trackedImage.referenceImage.name}");
            arObjects[trackedImage.referenceImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display tracked image name
        imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and place the object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

        Debug.Log($"Tracking image: {trackedImage.referenceImage.name}, Position: {trackedImage.transform.position}");
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if (arObjects.ContainsKey(name))
        {
            GameObject arObject = arObjects[name];
            arObject.SetActive(true);
            arObject.transform.position = newPosition;
            arObject.transform.localScale = scaleFactor;

            // Disable other objects
            foreach (KeyValuePair<string, GameObject> entry in arObjects)
            {
                if (entry.Key != name)
                {
                    entry.Value.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning($"No AR object found for name: {name}");
        }
    }
}
