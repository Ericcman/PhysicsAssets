using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceGoods : MonoBehaviour
{
    public GameObject Pricing;
    // Start is called before the first frame update
    void Start()
    {
        Pricing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pricing.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            Destroy(collision.gameObject);
            Pricing.SetActive(true);
        }
    }
}
