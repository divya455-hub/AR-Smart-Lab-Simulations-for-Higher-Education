using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController_EDTA : MonoBehaviour
{
    public ChemistryEngine_EDTA chem;    // assign EDTA_Manager
    public Renderer liquidRenderer;      // assign the Liquid renderer (cylinder)
    public Color wineRed = new Color(0.86f,0.08f,0.43f,0.75f);
    public Color blue = new Color(0.12f,0.45f,0.85f,0.75f);
    public float transitionSpeed = 4f;

    Material instMat;

    void Start()
    {
        if (liquidRenderer == null) Debug.LogError("[IndicatorController_EDTA] Assign liquidRenderer in Inspector.");
        else
        {
            instMat = new Material(liquidRenderer.sharedMaterial);
            liquidRenderer.material = instMat;
            instMat.SetColor("_Color", wineRed);
        }
    }

    void Update()
    {
        if (chem == null || instMat == null) return;

        // If indicator not added, keep neutral very faint color
        if (!chem.indicatorAdded)
        {
            Color neutral = new Color(0.95f, 0.95f, 0.95f, 0.35f);
            instMat.SetColor("_Color", Color.Lerp(instMat.GetColor("_Color"), neutral, Time.deltaTime * transitionSpeed));
            return;
        }

        // Calculate fraction toward endpoint
        float req = chem.RequiredVolumeForInitialHardness();
        float frac = req <= 0f ? 0f : Mathf.Clamp01(chem.titrantAdded_mL / req);

        Color target = Color.Lerp(wineRed, blue, frac);
        Color cur = instMat.GetColor("_Color");
        instMat.SetColor("_Color", Color.Lerp(cur, target, Time.deltaTime * transitionSpeed));
    }
}
