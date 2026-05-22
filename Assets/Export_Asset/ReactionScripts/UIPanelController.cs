using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    public GameObject moleculePrefab;
    public Transform moleculeSpawnPoint;
    public GameObject panel;

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void SpawnMolecule()
    {
        Instantiate(moleculePrefab, moleculeSpawnPoint.position, Quaternion.identity);
    }
}
