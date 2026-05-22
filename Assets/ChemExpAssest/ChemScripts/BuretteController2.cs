using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuretteController2 : MonoBehaviour
{
    public ChemistryEngine_Iron chem;
    public IronUI ironUI;

    public GameObject dropPrefab;
    public Transform dropSpawnPoint;

    public float dropVolume_mL = 0.05f;
    public float continuousRate = 0.5f;
    public bool isContinuous = false;

    private float timer = 0f;

    void Update()
    {
        if (isContinuous)
        {
            timer += Time.deltaTime;
            if (timer >= 1f / continuousRate)
            {
                ReleaseDrop();
                timer = 0f;
            }
        }
    }

    public void ReleaseDrop()
    {
        if (chem == null || dropPrefab == null || dropSpawnPoint == null) return;

        // 1. Add chemical logic
        chem.AddKMnO4(dropVolume_mL);
        ironUI.RefreshUI();

        // 2. Instantiate drop visually
        GameObject drop = Instantiate(dropPrefab, dropSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = drop.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * 0.3f, ForceMode.Impulse);

        // 3. Auto destroy after 3 seconds
        Destroy(drop, 3f);
    }

    public void StartContinuous() { isContinuous = true; }
    public void StopContinuous() { isContinuous = false; }
    public void SetContinuousRate(float rate) { continuousRate = rate; }
}
