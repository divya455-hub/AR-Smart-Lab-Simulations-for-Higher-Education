using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaterDropRepeater : MonoBehaviour
{
    public GameObject waterDropPrefab;  // Prefab of the water drop
    public Transform dropSpawnPoint;    // Position where drops appear (burette bottom)
    public Transform glassWater;        // Water inside the glass
    public Slider slider;               // UI Slider
    public Material waterMaterial;      // Material for color change

    public float dropInterval = 0.5f;   // Time between drops
    private Coroutine dropCoroutine;    // Stores coroutine instance
    private float fillAmount = 0.1f;    // Initial water level
    private float maxFill = 1f;         // Maximum water level
    private int dropCount = 0;
    private int endpoint = 20;          // Number of drops required to reach endpoint

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        if (value > 0.1f) // Start dropping when slider moves
        {
            if (dropCoroutine == null) // Start only if not already running
            {
                dropCoroutine = StartCoroutine(DropWaterContinuously());
            }
        }
        else
        {
            StopDropping();
        }
    }

    IEnumerator DropWaterContinuously()
    {
        while (true) // Infinite loop to keep spawning drops
        {
            GameObject drop = Instantiate(waterDropPrefab, dropSpawnPoint.position, Quaternion.identity);
            drop.tag = "WaterDrop"; // Ensure drops have the correct tag
            yield return new WaitForSeconds(dropInterval); // Wait before spawning next drop
        }
    }

    void StopDropping()
    {
        if (dropCoroutine != null)
        {
            StopCoroutine(dropCoroutine);
            dropCoroutine = null;
        }
    }

    // ✅ Make it PUBLIC so WaterTrigger.cs can access it
    public void IncreaseWaterLevel()
    {
        if (fillAmount < maxFill)
        {
            fillAmount += 0.05f;
            glassWater.localScale = new Vector3(glassWater.localScale.x, fillAmount, glassWater.localScale.z);
            dropCount++;
            UpdateWaterColor();
        }
    }

    // ✅ Make it PUBLIC so WaterTrigger.cs can access it
    public void UpdateWaterColor()
    {
        float progress = (float)dropCount / endpoint; // Get progress ratio
        waterMaterial.color = Color.Lerp(Color.blue, Color.red, progress); // Gradual color change
    }
}
