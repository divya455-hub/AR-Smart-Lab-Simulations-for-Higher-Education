using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaterDropController : MonoBehaviour
{
    public GameObject waterDropPrefab;  // Water drop prefab
    public Transform dropSpawnPoint;    // Spawn position (burette bottom)
    public Transform glassWater;        // Water inside the glass
    public Slider slider;               // UI Slider
    public Material waterMaterial;      // Water material for color change

    public float dropInterval = 0.5f;   // Time between drops
    private Coroutine dropCoroutine;    // Stores coroutine instance
    private float fillAmount = 0.1f;    // Initial water level
    private float maxFill = 1f;         // Maximum water level
    private int dropCount = 0;
    private int endpoint = 20;          // Drops needed to change color

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnSliderValueChanged(float value)

    {
        if (value > 0.1f) // Start dropping when slider moves
        {
            if (dropCoroutine == null) // Prevent multiple coroutines
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
        while (slider.value > 0.1f) // Keep dropping if slider is above 0.1
        {
            if (waterDropPrefab != null)
            {
                GameObject drop = Instantiate(waterDropPrefab, dropSpawnPoint.position, Quaternion.identity);
                drop.tag = "WaterDrop";

                // ✅ Add Rigidbody & Gravity
                Rigidbody rb = drop.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = drop.AddComponent<Rigidbody>();
                }
                rb.useGravity = true;

                // ✅ Add Collider to detect glass
                if (drop.GetComponent<SphereCollider>() == null)
                {
                    SphereCollider collider = drop.AddComponent<SphereCollider>();
                    collider.isTrigger = true; // Use trigger to detect collision with water
                }

                // ✅ Add WaterDrop script to detect collision
                if (drop.GetComponent<WaterDrop>() == null)
                {
                    drop.AddComponent<WaterDrop>().controller = this;
                }
            }

            yield return new WaitForSeconds(dropInterval); // Adjust drop speed if needed
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

    public void UpdateWaterColor()
    {
        if (waterMaterial != null) // ✅ Ensure material is assigned
        {
            float progress = (float)dropCount / endpoint;
            waterMaterial.color = Color.Lerp(Color.blue, Color.red, progress);
        }
    }
}
