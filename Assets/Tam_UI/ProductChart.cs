using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductChart : MonoBehaviour
{
    public GameObject productChartPanel;
    // Start is called before the first frame update
    void Start()
    {
        //making sure panel is not visible at start of game 
        productChartPanel.SetActive(false);
    }

    public void OpenChart()
    {
        productChartPanel.SetActive(true);
    }
    public void CloseChart()
    {
        productChartPanel.SetActive(false);
    }
}
