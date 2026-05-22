using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FluidZone : MonoBehaviour
{
    [Tooltip("Higher = thicker fluid (stronger damping)")]
    public float viscosity = 2f;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BallFluid bf = other.GetComponent<BallFluid>();
        if (bf != null)
        {
            bf.SetViscosity(viscosity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BallFluid bf = other.GetComponent<BallFluid>();
        if (bf != null)
        {
            bf.ResetViscosity();
        }
    }
}
