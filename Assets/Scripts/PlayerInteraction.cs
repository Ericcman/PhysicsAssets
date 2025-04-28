using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float grabRange = 3f;
    public GameObject hand;

    private Rigidbody grabbedObject;

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
                    AnimalProduce produce = hit.collider.GetComponent<AnimalProduce>();
                    if (produce != null)
                    {
                        produce.Collect();
                        return;
                    }

                    Plant plant = hit.collider.GetComponent<Plant>();
                    if (plant != null)
                    {
                        plant.Collect();
                        return;
                    }

                    Rest house = hit.collider.GetComponent<Rest>();
                    if (house != null )
                    {
                        house.GoSleep();
                        return;
                    }

                    // If no special script, grab the object
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        grabbedObject = rb;
                        grabbedObject.useGravity = false;
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

        // Release the object on mouse release
        if (Input.GetButtonUp("Fire1") && grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject = null;
        }
    }

    private void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward * grabRange;
            grabbedObject.velocity = (targetPos - grabbedObject.position) * 10f; // Move smoothly
        }
    }
}
