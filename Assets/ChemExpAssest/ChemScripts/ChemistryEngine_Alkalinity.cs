using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryEngine_Alkalinity : MonoBehaviour
{
    public float sampleVolume_mL = 50f;      // Vs
    public float acidNormality = 0.02f;      // N
    public float initialTotalAlkalinity_mgL = 200f; // set in inspector

    [HideInInspector] public float titrantAdded_mL = 0f;

    public float InitialEquivalents() {
        return (initialTotalAlkalinity_mgL * sampleVolume_mL) / 50000f;
    }
    public float EquivalentsAdded() {
        return (acidNormality * titrantAdded_mL) / 1000f;
    }
    public float EquivalentsRemaining() {
        return Mathf.Max(0f, InitialEquivalents() - EquivalentsAdded());
    }
    public float CalculateMgPerLFromTitrant(float used_mL) {
        return (used_mL * acidNormality * 50000f) / sampleVolume_mL;
    }
    public float GetSimulatedPH() {
        float eqInit = InitialEquivalents();
        if (eqInit <= 0f) return 7f;
        float fraction = EquivalentsRemaining() / eqInit; // 1 -> no acid, 0 -> full neutralized
        return Mathf.Lerp(3.5f, 9.0f, fraction); // maps to pH for visual
    }
}
