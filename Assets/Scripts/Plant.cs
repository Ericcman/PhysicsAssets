using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject cropPrefab;
    public bool hasBeenCollected = false;

    public void Collect()
    {
        hasBeenCollected = true;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        Instantiate(cropPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);

    }
}
