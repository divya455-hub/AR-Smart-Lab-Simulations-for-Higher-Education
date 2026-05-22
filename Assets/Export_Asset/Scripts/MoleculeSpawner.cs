using UnityEngine;
using UnityEngine.UI;

public class MoleculeSpawner : MonoBehaviour
{
    public GameObject moleculePrefab;
    public Transform spawnPoint;
    public Slider sizeSlider; // Assign this in the Inspector

    public void SpawnMolecule()
    {
        if (moleculePrefab != null && spawnPoint != null)
        {
            GameObject newMolecule = Instantiate(moleculePrefab, spawnPoint.position, Quaternion.identity);

            // Ensure the spawned molecule has MoleculeSizeController
            MoleculeSizeController sizeController = newMolecule.GetComponent<MoleculeSizeController>();
            if (sizeController != null && sizeSlider != null)
            {
                sizeController.AssignSlider(sizeSlider); // Correct way to assign slider
            }
            else
            {
                Debug.LogError("MoleculeSizeController or SizeSlider is missing!");
            }
        }
        else
        {
            Debug.LogError("MoleculePrefab or SpawnPoint is not assigned!");
        }
    }
}
