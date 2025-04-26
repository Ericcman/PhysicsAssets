using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float grabRange = 3f;
    public GameObject hand;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                hand.SetActive(true);

                if (Input.GetButtonDown("Fire1"))
                {
                    Plant plant = hit.collider.GetComponent<Plant>();
                    if (plant != null && !plant.hasBeenCollected)
                    {
                        plant.Collect();
                    }
                }
            }
            else
            {
                hand.SetActive(false);
            }
        }
        else
        {
            hand.SetActive(false);
        }
    }
}
