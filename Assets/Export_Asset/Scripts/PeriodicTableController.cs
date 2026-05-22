using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class PeriodicTableController : MonoBehaviour
{
    
  


    public List< Element> listofelements =new List<Element>();           // Array to store element data
    public GameObject modelContainer;    // Shared space for 3D models
    public TextMeshProUGUI descriptionText; // Text panel for descriptions

    private GameObject currentModel;
    private void Start()
    {
        foreach (Element element in listofelements)
        {
            element.elementButton.onClick.AddListener(() =>
            {
                OnElementButtonClicked(element.elementName);
            });
        }
    }// Currently displayed 3D model

    // Function to handle button clicks
    public void OnElementButtonClicked(string elementName)
    {
        // Find the corresponding element
       Element selectedElement = System.Array.Find(listofelements.ToArray(), e => e.elementName == elementName);

        if (selectedElement != null)
        {
            // Replace the current model
            if (currentModel != null)
            {
                Destroy(currentModel); // Destroy the old model
            }
            currentModel = Instantiate(selectedElement.modelPrefab, modelContainer.transform);
            currentModel.transform.localPosition = Vector3.zero; // Reset position to match the container

            // Update the description text
            descriptionText.text = selectedElement.description;
        }
        else
        {
            Debug.LogError("Element not found: " + elementName);
        }
    }
}
[System.Serializable]
public class Element
{
    public string elementName;         // Element name (e.g., H2)
    public GameObject modelPrefab;    // Prefab of the 3D model
    public string description;
    public Button elementButton;// Description of the element
}
