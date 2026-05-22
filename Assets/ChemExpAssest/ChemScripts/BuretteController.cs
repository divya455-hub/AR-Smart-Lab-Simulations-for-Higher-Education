using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuretteController : MonoBehaviour
{
    public ChemistryEngine_Alkalinity chem;
    public float dropVolume_mL = 0.05f;
    public float continuousRate_mLPerSec = 0.5f;
    bool continuous = false;

    void Update() {
        if (continuous && chem != null) {
            float add = continuousRate_mLPerSec * Time.deltaTime;
            chem.titrantAdded_mL += add;
        }
    }

    public void ReleaseDrop() {
        if (chem == null) return;
        chem.titrantAdded_mL += dropVolume_mL;
    }

    public void StartContinuous() {
        continuous = true;
    }
    public void StopContinuous() {
        continuous = false;
    }

    // optional for slider to change rate
    public void SetContinuousRate(float r) {
        continuousRate_mLPerSec = r;
    }
}

