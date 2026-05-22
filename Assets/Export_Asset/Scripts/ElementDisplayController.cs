/*using UnityEngine;
using UnityEngine.UI;

public class ElementDisplayController : MonoBehaviour
{
    public GameObject[] elementModels; // Array for 3D models
    public Transform modelSpawnPoint; // Spawn point for models
    public Text elementInfoText;      // UI Text for element details

    private GameObject currentModel; // Reference to the active model

    // Dictionary to store element information
    private Dictionary<string, string> elementData = new Dictionary<string, string>
    {
        { "H", "Hydrogen: The lightest element." },
        { "O", "Oxygen: Essential for breathing." },
        // Add more elements here
    };

    public void ShowElement(string elementName)
    {
        // Update the text
        if (elementData.ContainsKey(elementName))
        {
            elementInfoText.text = elementData[elementName];
        }

        // Replace the current 3D model
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // Instantiate the new model
        foreach (GameObject model in elementModels)
        {
            if (model.name == elementName)
            {
                currentModel = Instantiate(model, modelSpawnPoint.position, Quaternion.identity);
                break;
            }
        }
    }
}
*/