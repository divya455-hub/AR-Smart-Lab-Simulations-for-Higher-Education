using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPortalPlacement : MonoBehaviour
{
    public GameObject portalPrefab; // Your AR Portal prefab
    public ARRaycastManager raycastManager; // Reference to the ARRaycastManager
    public ARPlaneManager planeManager; // Reference to the ARPlaneManager

    private GameObject spawnedPortal; // The instantiated portal
    private bool portalPlaced = false; // Flag to track if the portal has been placed

    void Update()
    {
        // Return if the portal has already been placed
        if (portalPlaced) return;

        // Check if the screen is being touched
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast to detect if a plane is hit
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Get the hit point
                    Pose hitPose = hits[0].pose;

                    // Place the portal on the detected plane if not already placed
                    if (!portalPlaced)
                    {
                        // Instantiate the portal prefab at the hit location
                        spawnedPortal = Instantiate(portalPrefab, hitPose.position, hitPose.rotation);
                        portalPlaced = true; // Set the flag to true

                        // Optionally, disable plane detection to prevent further placements
                        planeManager.enabled = false;
                    }
                }
            }
        }
    }
}