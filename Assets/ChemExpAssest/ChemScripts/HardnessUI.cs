using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class HardnessUI : MonoBehaviour
{
    public ChemistryEngine_EDTA chem;           // assign EDTA_Manager
    public BuretteController burette;           // optional: assign Burette object

    public TextMeshProUGUI edtaUsedText;        // assign EDTAUsedText UI
    public TextMeshProUGUI hardnessText;        // assign HardnessText UI
    public TextMeshProUGUI statusText;          // assign StatusText UI

    void Start()
    {
        if (chem == null) Debug.LogError("[HardnessUI] chem (EDTA_Manager) not assigned.");
    }

    void Update()
    {
        // keep Update for polling fallback
        RefreshUI();
    }

    // Call this after each drop for immediate update
    public void RefreshUI()
    {
        if (chem == null) return;

        if (edtaUsedText != null)
            edtaUsedText.text = "EDTA used: " + chem.titrantAdded_mL.ToString("F2") + " mL";

        if (chem.endpointReached)
        {
            if (hardnessText != null)
                hardnessText.text = "Hardness: " + chem.calculatedHardness_mgL.ToString("F2") + " mg/L as CaCO3";
            if (statusText != null)
                statusText.text = $"Endpoint reached ({chem.endpointVolume_mL:F2} mL)";
        }
        else
        {
            if (hardnessText != null) hardnessText.text = "Hardness: --";
            if (statusText != null) statusText.text = chem.bufferAdded && chem.indicatorAdded ? "Titrating..." : "Add buffer & indicator";
        }
    }

    public void SaveResults(string sampleId = "Sample1")
    {
        if (chem == null) { Debug.LogWarning("[HardnessUI] SaveResults called but chem is null."); return; }

        string file = Path.Combine(Application.persistentDataPath, "EDTAResults.csv");
        bool writeHeader = !File.Exists(file);
        using (StreamWriter sw = new StreamWriter(file, true))
        {
            if (writeHeader) sw.WriteLine("SampleID,EDTA_mL,Hardness_mgL");
            sw.WriteLine($"{sampleId},{chem.endpointVolume_mL:F2},{chem.calculatedHardness_mgL:F2}");
        }
        Debug.Log("[HardnessUI] Saved: " + file);
    }
}
