using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public Camera playerCamera;
    public float grabRange = 3f;
    private Rigidbody grabbedObject;
    private Vector3 offset;
    public GameObject hand;
    private Rigidbody rb;
    private void Start()
    {
        hand.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        if ((Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabRange)) && (hit.collider.CompareTag("Interactable")))
        {
            hand.SetActive(true);
            if (Input.GetButtonDown("Fire1")) // M1 to grab
        {

            if (hit.collider.CompareTag("Interactable"))
            {
                    grabbedObject = hit.collider.GetComponent<Rigidbody>();
                    grabbedObject.useGravity = false; // Disable gravity for smooth movement
                }

        }
    }
        else
        {
            hand.SetActive(false);
        }


        if (Input.GetButtonUp("Fire1") && grabbedObject != null) // Release
        {
            grabbedObject.useGravity = true;
            grabbedObject = null;
        }

        
    }

    void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward * grabRange;
            grabbedObject.velocity = (targetPos - grabbedObject.position) * 10f; // Smooth movement
        }
    }
}