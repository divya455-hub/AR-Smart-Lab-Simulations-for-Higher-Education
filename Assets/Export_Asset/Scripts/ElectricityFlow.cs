using UnityEngine;

public class ElectricityFlow : MonoBehaviour
{
    public bool isElectricityFlowstop=false;
    public float scrollSpeed = 1.0f;
    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if(!isElectricityFlowstop){
            // Calculate texture offset based on time
        offset = new Vector2(0, Time.time * scrollSpeed);

        // Apply the offset to the material
        rend.material.mainTextureOffset = offset;

        // Optional: Add subtle pulse effect
        float pulse = Mathf.PingPong(Time.time * 0.5f, 0.3f) + 0.7f;
        rend.material.SetFloat("_EmissionIntensity", pulse);

        }
        
    }
}
