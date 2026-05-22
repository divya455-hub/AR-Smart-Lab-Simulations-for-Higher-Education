using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class TitrationUI : MonoBehaviour
{
    [Header("Refs")]
    public ChemistryEngine_Alkalinity chem;
    public BuretteController burette;

    [Header("UI (TextMeshPro)")]
    public TextMeshProUGUI pHText;
    public TextMeshProUGUI titrantText;
    public TextMeshProUGUI V1Text;
    public TextMeshProUGUI V2Text;
    public TextMeshProUGUI totalVText;
    public TextMeshProUGUI phenolAlkText;
    public TextMeshProUGUI totalAlkText;

    [Header("Thresholds")]
    public float phenolThreshold = 8.3f;
    public float methylThreshold = 4.5f;

    bool phenolRecorded = false;
    float V1 = 0f;
    float Vtotal = 0f;

    void Start()
    {
        if (chem == null) Debug.LogError("Assign ChemistryEngine_Alkalinity in inspector.");
        // if initial pH below phenol threshold, set V1 = 0 immediately
        if (chem.GetSimulatedPH() < phenolThreshold) {
            phenolRecorded = true;
            V1 = 0f;
            V1Text.text = "V1: 0.00 mL";
        }
        UpdateUI();
    }

    void Update()
    {
        if (chem == null) return;
        float pH = chem.GetSimulatedPH();
        float used = chem.titrantAdded_mL;

        // update pH and titrant used
        pHText.text = "pH: " + pH.ToString("F2");
        titrantText.text = "Titrant: " + used.ToString("F2") + " mL";

        // detect phenolphthalein endpoint crossing (first time pH drops below threshold)
        if (!phenolRecorded && pH < phenolThreshold) {
            phenolRecorded = true;
            V1 = used;
            V1Text.text = "V1: " + V1.ToString("F2") + " mL";
            float phenolMg = chem.CalculateMgPerLFromTitrant(V1);
            phenolAlkText.text = "Phenol Alk: " + phenolMg.ToString("F2") + " mg/L";
        }

        // detect methyl orange / total alkalinity endpoint (first time pH <= methylThreshold)
        if (Vtotal == 0f && pH <= methylThreshold) {
            Vtotal = used;
            V2Text.text = "V2 (extra): " + (Vtotal - V1).ToString("F2") + " mL";
            totalVText.text = "Vtotal: " + Vtotal.ToString("F2") + " mL";
            float totalMg = chem.CalculateMgPerLFromTitrant(Vtotal);
            totalAlkText.text = "Total Alk: " + totalMg.ToString("F2") + " mg/L";
        }
    }

    void UpdateUI()
    {
        // safe-init UI text
        if (V1Text) V1Text.text = "V1: --";
        if (V2Text) V2Text.text = "V2 (extra): --";
        if (totalVText) totalVText.text = "Vtotal: --";
        if (phenolAlkText) phenolAlkText.text = "Phenol Alk: --";
        if (totalAlkText) totalAlkText.text = "Total Alk: --";
    }

    // Call this from a UI button to save results
    public void SaveResults(string sampleId = "Sample1")
    {
        string folder = Application.persistentDataPath;
        string file = Path.Combine(folder, "AlkResults.csv");
        bool writeHeader = !File.Exists(file);

        using (StreamWriter sw = new StreamWriter(file, true)) {
            if (writeHeader) sw.WriteLine("SampleID,V1_mL,Vtotal_mL,PhenolAlk_mgL,TotalAlk_mgL");
            float phenolMg = V1 > 0 ? chem.CalculateMgPerLFromTitrant(V1) : 0f;
            float totalMg = Vtotal > 0 ? chem.CalculateMgPerLFromTitrant(Vtotal) : 0f;
            sw.WriteLine($"{sampleId},{V1:F2},{Vtotal:F2},{phenolMg:F2},{totalMg:F2}");
        }

        Debug.Log($"Saved results to {file}");
    }

    // Reset experiment (optional)
    public void ResetExperiment()
    {
        phenolRecorded = false;
        V1 = 0f;
        Vtotal = 0f;
        chem.titrantAdded_mL = 0f;
        UpdateUI();
    }
}
