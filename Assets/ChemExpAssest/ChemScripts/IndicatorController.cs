using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    [Header("References")]
    public ChemistryEngine_Alkalinity chem;   // assign in inspector
    public Renderer liquidRenderer;           // Liquid Mesh Renderer

    [Header("Colors & thresholds")]
    public Color neutralColor = new Color(0.95f, 0.96f, 1f, 0.45f);
    public Color phenolPink = new Color(1f, 0.4f, 0.65f, 0.6f);
    public Color methylOrange = new Color(1f, 0.55f, 0.15f, 0.6f);
    public float phenolThreshold = 8.3f;
    public float methylThreshold = 4.5f;
    public float transitionSpeed = 3f;

    Material instancedMat;

    void Start()
    {
        if (liquidRenderer == null) Debug.LogError("Liquid Renderer not set on IndicatorController.");
        // create instance so we don't modify shared material
        instancedMat = new Material(liquidRenderer.sharedMaterial);
        liquidRenderer.material = instancedMat;
        // initialize color
        instancedMat.SetColor("_Color", neutralColor);
    }

    void Update()
    {
        if (chem == null) return;
        float pH = chem.GetSimulatedPH();
        Color target = neutralColor;

        if (pH >= phenolThreshold) target = phenolPink;
        else if (pH <= methylThreshold) target = methylOrange;
        else target = neutralColor;

        Color current = instancedMat.GetColor("_Color");
        Color next = Color.Lerp(current, target, Time.deltaTime * transitionSpeed);
        instancedMat.SetColor("_Color", next);
    }
}
