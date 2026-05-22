using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuretteController1 : MonoBehaviour
{
    [Header("References")]
    public ChemistryEngine_EDTA chem;           // assign EDTA_Manager
    public HardnessUI hardnessUI;              // assign UI_Manager (HardnessUI)

    [Header("Dispense Settings")]
    public float dropVolume_mL = 0.05f;         // single tap drop
    public float continuousRate_mLPerSec = 0.5f;// mL per second while holding

    bool continuous = false;

    void Update()
    {
        if (continuous && chem != null)
        {
            float add = continuousRate_mLPerSec * Time.deltaTime;
            chem.AddEDTA(add);
            // update UI immediately
            if (hardnessUI != null) hardnessUI.RefreshUI();
        }
    }

    public void ReleaseDrop()
    {
        Debug.Log("[BuretteController] ReleaseDrop called");
        if (chem == null)
        {
            Debug.LogWarning("[BuretteController] chem reference is null.");
            return;
        }

        chem.AddEDTA(dropVolume_mL);
        Debug.Log($"[BuretteController] After ReleaseDrop total EDTA = {chem.titrantAdded_mL:F3} mL");

        if (hardnessUI != null) hardnessUI.RefreshUI();
    }

    public void StartContinuous() { continuous = true; Debug.Log("[BuretteController] Continuous started"); }
    public void StopContinuous() { continuous = false; Debug.Log("[BuretteController] Continuous stopped"); }

    public void SetContinuousRate(float r)
    {
        continuousRate_mLPerSec = Mathf.Max(0f, r);
        Debug.Log($"[BuretteController] SetContinuousRate = {continuousRate_mLPerSec:F3} mL/s");
    }
}
