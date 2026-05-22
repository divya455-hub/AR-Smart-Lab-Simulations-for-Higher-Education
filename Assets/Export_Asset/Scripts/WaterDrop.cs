using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public WaterDropController controller;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glass")) // ✅ Detect when drop reaches glass
        {
            controller.IncreaseWaterLevel(); // Increase water level
            Destroy(gameObject); // Destroy drop
        }
    }
}
