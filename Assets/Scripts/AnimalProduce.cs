using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalProduce : MonoBehaviour
{
    public GameObject AnimalGoods;
    public bool hasBeenCollected = false;
    public Rest house;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Collect()
    {
        if (hasBeenCollected == false)
        {
            hasBeenCollected = true;

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            Instantiate(AnimalGoods, transform.position + Vector3.right * -8f + Vector3.up * 3f, Quaternion.identity);
        }
    }
}
