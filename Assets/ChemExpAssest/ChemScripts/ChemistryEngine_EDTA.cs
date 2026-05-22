using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryEngine_EDTA : MonoBehaviour
{
    [Header("Experiment Settings")]
    public float sampleVolume_mL = 50f;    // Vs
    public float edtaMolarity = 0.01f;     // M (if using molarity)
    public float mgCaCO3_per_mL = 0f;      // if standardized (set >0 to use)
    public float initialHardness_mgL = 200f; // simulation value (teacher)

    [HideInInspector] public float titrantAdded_mL = 0f;
    [HideInInspector] public bool bufferAdded = false;
    [HideInInspector] public bool indicatorAdded = false;

    [HideInInspector] public bool endpointReached = false;
    [HideInInspector] public float endpointVolume_mL = 0f;
    [HideInInspector] public float calculatedHardness_mgL = 0f;

    // Add EDTA (called from BuretteController)
    public void AddEDTA(float ml)
    {
        if (!bufferAdded || !indicatorAdded)
        {
            Debug.LogWarning("[ChemistryEngine_EDTA] AddEDTA blocked — add buffer and indicator first.");
            return;
        }

        if (endpointReached)
        {
            Debug.Log("[ChemistryEngine_EDTA] Endpoint already reached; no more EDTA will be accepted.");
            return;
        }

        titrantAdded_mL += ml;
        Debug.Log($"[ChemistryEngine_EDTA] Added {ml:F3} mL; total = {titrantAdded_mL:F3} mL");

        CheckEndpoint();
    }

    // Calculate hardness from a given titrant used (ml)
    public float CalculateHardnessFromTitrant(float used_mL)
    {
        if (mgCaCO3_per_mL > 0f)
            return used_mL * mgCaCO3_per_mL * (1000f / sampleVolume_mL);
        else
            return (used_mL * edtaMolarity * 100000f) / sampleVolume_mL;
    }

    // Required volume to neutralize initialHardness (for simulation)
    public float RequiredVolumeForInitialHardness()
    {
        if (initialHardness_mgL <= 0f) return 0f;
        if (mgCaCO3_per_mL > 0f)
            return (initialHardness_mgL * sampleVolume_mL) / (mgCaCO3_per_mL * 1000f);
        else
            return (initialHardness_mgL * sampleVolume_mL) / (edtaMolarity * 100000f);
    }

    void CheckEndpoint()
    {
        float required = RequiredVolumeForInitialHardness();
        if (required <= 0f) return;

        if (titrantAdded_mL >= required && !endpointReached)
        {
            endpointReached = true;
            endpointVolume_mL = titrantAdded_mL;
            calculatedHardness_mgL = CalculateHardnessFromTitrant(endpointVolume_mL);
            Debug.Log($"[ChemistryEngine_EDTA] Endpoint reached at {endpointVolume_mL:F3} mL. Hardness: {calculatedHardness_mgL:F3} mg/L");
        }
    }

    // UI helper methods (callable from Button.OnClick)
    public void AddBuffer() { bufferAdded = true; Debug.Log("[ChemistryEngine_EDTA] Buffer added."); }
    public void AddIndicator() { indicatorAdded = true; Debug.Log("[ChemistryEngine_EDTA] Indicator added."); }

    public void ResetExperiment()
    {
        titrantAdded_mL = 0f;
        bufferAdded = false;
        indicatorAdded = false;
        endpointReached = false;
        endpointVolume_mL = 0f;
        calculatedHardness_mgL = 0f;
        Debug.Log("[ChemistryEngine_EDTA] Experiment reset.");
    }
}
