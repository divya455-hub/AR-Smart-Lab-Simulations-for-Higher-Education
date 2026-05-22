using UnityEngine;
using UnityEngine.UI;

public class MoleculeSizeController : MonoBehaviour
{
    private Slider sizeSlider; // Make it private to avoid inspector issues

    public void AssignSlider(Slider slider)
    {
        sizeSlider = slider;
        if (sizeSlider != null)
        {
            sizeSlider.onValueChanged.AddListener(ChangeSize);
            ChangeSize(sizeSlider.value); // Apply initial size
        }
        else
        {
            Debug.LogError("SizeSlider is null in MoleculeSizeController!");
        }
    }

    void ChangeSize(float value)
    {
        transform.localScale = Vector3.one * Mathf.Lerp(0.5f, 2.0f, value);
    }
}
