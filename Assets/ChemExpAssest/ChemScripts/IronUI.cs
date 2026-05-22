using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class IronUI : MonoBehaviour
{
    public ChemistryEngine_Iron chem;
    public BuretteController burette;

    public TextMeshProUGUI kmnUsedText;
    public TextMeshProUGUI feText;
    public TextMeshProUGUI statusText;

    void Start()
    {
        if (chem == null) Debug.LogError("[IronUI] chem not assigned.");
    }

    void Update() { RefreshUI(); }

    public void RefreshUI()
    {
        if (chem == null) return;
        if (kmnUsedText != null) kmnUsedText.text = "KMnO4 used: " + chem.titrantAdded_mL.ToString("F2") + " mL";
        if (chem.endpointReached)
        {
            if (feText != null) feText.text = "Fe: " + chem.calculatedFe_mgL.ToString("F2") + " mg/L";
            if (statusText != null) statusText.text = $"Endpoint reached ({chem.endpointVolume_mL:F2} mL)";
        }
        else
        {
            if (feText != null) feText.text = "Fe: --";
            if (statusText != null) statusText.text = chem.acidAdded && chem.sampleAdded && chem.kmno4Added ? "Titrating..." : "Add acid, sample, KMnO4";
        }
    }

    public void SaveResults(string sampleId = "Sample1")
    {
        if (chem == null) return;
        string file = Path.Combine(Application.persistentDataPath, "IronResults.csv");
        bool writeHeader = !File.Exists(file);
        using (StreamWriter sw = new StreamWriter(file, true))
        {
            if (writeHeader) sw.WriteLine("SampleID,KMnO4_mL,Fe_mgL");
            sw.WriteLine($"{sampleId},{chem.endpointVolume_mL:F2},{chem.calculatedFe_mgL:F2}");
        }
        Debug.Log("[IronUI] Saved " + file);
    }
}
