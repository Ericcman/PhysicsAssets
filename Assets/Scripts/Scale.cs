using UnityEngine;
using TMPro;

public class Scale : MonoBehaviour
{
    [SerializeField] private TextMeshPro massDisplay;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null && massDisplay != null)
        {
            float mass = rb.mass;
            massDisplay.text = mass.ToString("F2");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null && massDisplay != null)
        {
            massDisplay.text = "0.00";
        }
    }
}
