using UnityEngine;
using UnityEngine.UI;

public class DragLaser : MonoBehaviour
{
    public Text measurement1Text;
    public Text measurement2Text;
    public Text measurement3Text;
    public GameObject measurementPanel;  // Parent panel holding all measurements
    public Button closeButton;

    private float zCoord;
    private bool isDragging = false;
    private Camera mainCamera;
    private float initialY;
    private float initialZ;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("No Main Camera found in the scene!");
        }

        initialY = transform.position.y;
        initialZ = transform.position.z;

        // Add close button listener
        if (closeButton != null)
            closeButton.onClick.AddListener(HideMeasurements);
    }

    void Update()
    {
        if (mainCamera == null || measurementPanel == null || !measurementPanel.activeSelf) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    zCoord = mainCamera.WorldToScreenPoint(transform.position).z;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                float newX = GetWorldXPosition(touch.position);
                transform.position = new Vector3(newX, initialY, initialZ);
                UpdateMeasurements(newX);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }

    private float GetWorldXPosition(Vector2 screenPosition)
    {
        Vector3 screenPoint = new Vector3(screenPosition.x, screenPosition.y, zCoord);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
        return worldPoint.x;
    }

    private void UpdateMeasurements(float xPos)
    {
        float baseValue = 10f;
        float spacing = 1.5f;

        if (measurement1Text != null)
            measurement1Text.text = $"M1: {(baseValue + xPos * spacing):F2} cm";

        if (measurement2Text != null)
            measurement2Text.text = $"M2: {(baseValue + (xPos + 0.1f) * spacing):F2} cm";

        if (measurement3Text != null)
            measurement3Text.text = $"M3: {(baseValue + (xPos - 0.1f) * spacing):F2} cm";
    }

    private void HideMeasurements()
    {
        if (measurementPanel != null)
            measurementPanel.SetActive(false);
    }
}
