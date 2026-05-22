using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlanes : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    UnityEvent placementUpdate;

    [SerializeField]
    GameObject visualObject;

    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject spawnedObject { get; private set; }

    private bool placementDone = false;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();

        if (placementUpdate == null)
            placementUpdate = new UnityEvent();

        placementUpdate.AddListener(DisablePlaneVisualization);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (placementDone || !TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                placementDone = true;
                placementUpdate.Invoke(); // Disable further placement and tracking
            }
        }
    }

    public void DisablePlaneVisualization()
    {
        if (visualObject != null)
        {
            visualObject.SetActive(false);
        }

        // Disable AR plane tracking
        var arPlaneManager = FindObjectOfType<ARPlaneManager>();
        if (arPlaneManager != null)
        {
            foreach (var plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false); // Disable existing planes
            }
            arPlaneManager.enabled = false; // Stop detecting new planes
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    ARRaycastManager m_RaycastManager;
}
