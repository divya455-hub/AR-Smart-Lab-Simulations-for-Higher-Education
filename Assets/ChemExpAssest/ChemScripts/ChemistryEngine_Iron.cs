using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryEngine_Iron : MonoBehaviour
{
    [Header("Experiment settings")]
    public float sampleVolume_mL = 50f;       // Vs
    public float permanganateNormality = 0.02f; // N
    public float initialIron_mgL = 200f;     // simulation only

    [HideInInspector] public float titrantAdded_mL = 0f;
    [HideInInspector] public bool acidAdded = false;
    [HideInInspector] public bool sampleAdded = false;
    [HideInInspector] public bool kmno4Added = false;

    [HideInInspector] public bool endpointReached = false;
    [HideInInspector] public float endpointVolume_mL = 0f;
    [HideInInspector] public float calculatedFe_mgL = 0f;

    // Add titrant (called from BuretteController)
    public void AddKMnO4(float ml)
    {
        // require acid and sample only (don't block on kmno4Added)
        if (!acidAdded || !sampleAdded)
        {
            Debug.LogWarning("[ChemistryEngine_Iron] AddKMnO4 blocked — ensure acid and sample are added.");
            return;
        }
        if (endpointReached) return;

        titrantAdded_mL += ml;

        // DON'T flip kmno4Added here — keep it controlled by AddKMnO4Std()
        // kmno4Added = true; // removed

        CheckEndpoint();
    }

    // Calculate Fe conc from titrant volume used
    public float CalculateFeFromTitrant(float used_mL)
    {
        return (used_mL * permanganateNormality * 55850f) / sampleVolume_mL;
    }

    // Required volume (for simulation) given initialIron_mgL
    public float RequiredVolumeForInitialFe()
    {
        if (permanganateNormality <= 0f) return 0f;
        return (initialIron_mgL * sampleVolume_mL) / (permanganateNormality * 55850f);
    }

    void CheckEndpoint()
    {
        float required = RequiredVolumeForInitialFe();
        if (required <= 0f) return;
        if (titrantAdded_mL >= required && !endpointReached)
        {
            endpointReached = true;
            endpointVolume_mL = titrantAdded_mL;
            calculatedFe_mgL = CalculateFeFromTitrant(endpointVolume_mL);
            Debug.Log($"[ChemistryEngine_Iron] Endpoint reached: V={endpointVolume_mL:F3} mL, Fe={calculatedFe_mgL:F3} mg/L");
        }
    }

    // UI-callable helpers
    public void AddAcid() { acidAdded = true; Debug.Log("[ChemistryEngine_Iron] Acid added."); }
    public void AddSample() { sampleAdded = true; Debug.Log("[ChemistryEngine_Iron] Sample added."); }
    public void AddKMnO4Std() { kmno4Added = true; Debug.Log("[ChemistryEngine_Iron] KMnO4 std ready."); }
    public void ResetExperiment()
    {
        titrantAdded_mL = 0f; acidAdded = false; sampleAdded = false; kmno4Added = false;
        endpointReached = false; endpointVolume_mL = 0f; calculatedFe_mgL = 0f;
        Debug.Log("[ChemistryEngine_Iron] Reset.");
    }
}
