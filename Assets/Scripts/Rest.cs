using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoSleep()
    {
        Debug.Log("You Slept");
        StartCoroutine(Fadeout());
    }

    IEnumerator Fadeout()
    {
        // Fade to black over 1 second
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Make sure it's fully black (in case of rounding)
        fadeImage.color = new Color(0, 0, 0, 1);

        // OPTIONAL: fade back in
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsed / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Make sure it's fully transparent again
        fadeImage.color = new Color(0, 0, 0, 0);
    }
}
