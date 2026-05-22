using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController_Iron : MonoBehaviour
{
    public ChemistryEngine_Iron chem;
    public Renderer liquidRenderer;
    public Transform liquidTransform;
    public float minHeight = 0.1f;
    public float maxHeight = 0.6f;

    // initial white (colorless-looking) and final pink
    public Color neutralColor = new Color(1f, 1f, 1f, 0.95f);   // white/colorless
    public Color endPink = new Color(0.8f, 0.05f, 0.6f, 0.95f); // KMnO4-like
    public float transitionSpeed = 4f;

    private Material instMat;
    private Shader matShader;

    void Start()
    {
        if (liquidRenderer == null)
        {
            Debug.LogError("[Indicator] Assign liquidRenderer in inspector.");
            return;
        }
        if (liquidTransform == null)
        {
            Debug.LogError("[Indicator] Assign liquidTransform in inspector.");
            return;
        }
        if (chem == null)
        {
            Debug.LogWarning("[Indicator] ChemistryEngine_Iron not assigned — assigning will help.");
        }
        else
        {
            // force titrant to zero at start to ensure starting state
            chem.titrantAdded_mL = 0f;
        }

        // make unique instance so we don't alter the shared material permanently
        instMat = new Material(liquidRenderer.sharedMaterial ?? liquidRenderer.material);
        liquidRenderer.material = instMat;

        matShader = instMat.shader;
        Debug.Log($"[Indicator] Liquid shader: {matShader.name}");

        // Ensure initial color is white/colorless on common property names
        SetMaterialColorProperty(neutralColor);
        // Turn off emission if present (prevents glowing pink)
        if (instMat.HasProperty("_EmissionColor"))
        {
            instMat.SetColor("_EmissionColor", Color.black);
            instMat.DisableKeyword("_EMISSION");
        }

        // If using URP/Universal, try _BaseColor too
        if (instMat.HasProperty("_BaseColor")) instMat.SetColor("_BaseColor", neutralColor);

        SetLiquidHeight(0f);

        // Log the actual color values we set (helps debug)
        Color read = ReadMaterialColor();
        Debug.Log($"[Indicator] Initial material color (read): {read}");
    }

    void Update()
    {
        if (chem == null || instMat == null || liquidTransform == null) return;

        float required = chem.RequiredVolumeForInitialFe();
        float fraction = required <= 0f ? 0f : Mathf.Clamp01(chem.titrantAdded_mL / required);

        // If no titrant added - lock to neutral exactly (prevents accidental pink)
        if (chem.titrantAdded_mL <= 0f)
        {
            // set quickly toward neutral
            SetMaterialColorLerp(neutralColor, Time.deltaTime * transitionSpeed * 5f);
        }
        else
        {
            Color targetColor = Color.Lerp(neutralColor, endPink, fraction);
            SetMaterialColorLerp(targetColor, Time.deltaTime * transitionSpeed);
        }

        // Fill level animation
        float targetHeight = Mathf.Lerp(minHeight, maxHeight, fraction);
        Vector3 scale = liquidTransform.localScale;
        scale.y = Mathf.Lerp(scale.y, targetHeight, Time.deltaTime * transitionSpeed);
        liquidTransform.localScale = scale;
        Vector3 pos = liquidTransform.localPosition;
        pos.y = scale.y / 2f;
        liquidTransform.localPosition = pos;
    }

    // Helper: set color on common property names
    void SetMaterialColorProperty(Color c)
    {
        if (instMat.HasProperty("_Color")) instMat.SetColor("_Color", c);
        if (instMat.HasProperty("_BaseColor")) instMat.SetColor("_BaseColor", c);
        if (instMat.HasProperty("_TintColor")) instMat.SetColor("_TintColor", c);
        if (instMat.HasProperty("_MainColor")) instMat.SetColor("_MainColor", c);
        // also try material.color fallback
        instMat.color = c;
    }

    // Helper: lerp color across properties
    void SetMaterialColorLerp(Color target, float t)
    {
        Color current = ReadMaterialColor();
        Color next = Color.Lerp(current, target, t);
        SetMaterialColorProperty(next);
        // also ensure emission off
        if (instMat.HasProperty("_EmissionColor"))
        {
            instMat.SetColor("_EmissionColor", Color.black);
        }
    }

    // Try to read color from commonly used property (for logging)
    Color ReadMaterialColor()
    {
        if (instMat.HasProperty("_Color")) return instMat.GetColor("_Color");
        if (instMat.HasProperty("_BaseColor")) return instMat.GetColor("_BaseColor");
        if (instMat.HasProperty("_TintColor")) return instMat.GetColor("_TintColor");
        return instMat.color;
    }

    void SetLiquidHeight(float fraction)
    {
        float height = Mathf.Lerp(minHeight, maxHeight, fraction);
        Vector3 scale = liquidTransform.localScale;
        scale.y = height;
        liquidTransform.localScale = scale;
        Vector3 pos = liquidTransform.localPosition;
        pos.y = height / 2f;
        liquidTransform.localPosition = pos;
    }
}
