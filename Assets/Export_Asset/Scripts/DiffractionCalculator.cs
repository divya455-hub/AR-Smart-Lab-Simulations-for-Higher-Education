using UnityEngine;
using TMPro;

public class DiffractionCalculator : MonoBehaviour
{
    public TMP_InputField distanceDInput;
    public TMP_InputField distanceX1Input;
    public TMP_InputField distanceX2Input;

    public TMP_Text outputX;
    public TMP_Text outputSinTheta;

    public void CalculateDiffraction()
    {
        if (float.TryParse(distanceDInput.text, out float D) &&
            float.TryParse(distanceX1Input.text, out float x1) &&
            float.TryParse(distanceX2Input.text, out float x2))
        {
            float x = (x1 + x2) / 2;
            float sinTheta = x / Mathf.Sqrt(x * x + D * D);

            outputX.text = "x = " + x.ToString("F2");
            outputSinTheta.text = "sin theta = " + sinTheta.ToString("F4");
        }
        else
        {
            outputX.text = "Invalid Input";
            outputSinTheta.text = "Invalid Input";
        }
    }
}
