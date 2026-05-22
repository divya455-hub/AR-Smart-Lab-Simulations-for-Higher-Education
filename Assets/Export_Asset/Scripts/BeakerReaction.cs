using UnityEngine;

public class BeakerReaction : MonoBehaviour
{
    public Renderer waterRenderer; // Assign the water material in Inspector
    public ParticleSystem bubbles; // Assign the bubble effect in Inspector

    public Color normalColor = Color.blue;
    public Color lightColor = new Color(0.5f, 0.5f, 1f); // Lighter shade
    public Color darkColor = new Color(0f, 0f, 0.5f); // Darker shade
    public Color reactionColor = Color.red;

    public float maxAllowedSize = 1.5f;
    public float explosionThreshold = 1.8f;

    void Start()
    {
        if (bubbles != null)
            bubbles.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mercury")) // Ensure Mercury prefab has "Mercury" tag
        {
            float moleculeSize = other.transform.localScale.x;

            // Water color change logic
            if (moleculeSize < 0.5f)
            {
                waterRenderer.material.color = lightColor; // Small mercury = Light color
            }
            else if (moleculeSize >= 0.5f)
            {
                waterRenderer.material.color = darkColor; // Large mercury = Dark color
            }

            // Reaction and explosion logic
            ReactToMolecule(moleculeSize);
        }
    }

    void ReactToMolecule(float moleculeSize)
    {
        if (moleculeSize > explosionThreshold)
        {
            ExplodeBeaker();
            return;
        }

        // Adjust water color intensity based on molecule size
        float intensity = Mathf.InverseLerp(0.5f, maxAllowedSize, moleculeSize);
        waterRenderer.material.color = Color.Lerp(normalColor, reactionColor, intensity);

        // Adjust bubble effect
        if (bubbles != null)
        {
            var emission = bubbles.emission;
            emission.rateOverTime = Mathf.Lerp(10, 100, intensity);

            if (!bubbles.isPlaying)
                bubbles.Play();
        }
    }

    void ExplodeBeaker()
    {
        Debug.Log("Boom! Molecule level too high!");
        Destroy(gameObject); // Destroy Beaker
    }
}
