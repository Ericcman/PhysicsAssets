using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PriceValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField priceInputField;
    [SerializeField] private TextMeshProUGUI resultText;


    public void ValidatePrice()
    {
        // Try to parse the input as a float
        if (float.TryParse(priceInputField.text, out float price))
        {
            if (price > 10f)
            {
                Debug.Log("price is too high! this will never sell");
                resultText.text = "price is too high! this will never sell";
            }
            else
            {
                Debug.Log("what a steal! that's super low in this economy!");
                resultText.text = "what a steal! that's super low in this economy!";
            }
        }
        else
        {
            Debug.LogError("Invalid price input!");
            resultText.text = "Please enter a valid price";
        }
    }
}
