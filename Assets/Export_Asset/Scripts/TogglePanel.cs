using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject moleculePanel;

    public void ToggleMoleculePanel()
    {
        moleculePanel.SetActive(!moleculePanel.activeSelf);
    }
}
