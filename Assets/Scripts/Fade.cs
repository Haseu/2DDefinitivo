using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject darkPanel;
    public Image darkImage;
    public Color[] transictionColor;
    public float step;
    private bool transiction;

    public void fadeIn()
    {
        if(!transiction){
            darkPanel.SetActive(true);
            StartCoroutine("cFadeIn");
        }
    }

    public void fadeOut()
    {
        StartCoroutine("cFadeOut");
    }

    private IEnumerator cFadeIn()
    {
        transiction = true;
        for (float i = 0; i < 1; i += step)
        {
            darkImage.color = Color.Lerp(transictionColor[0], transictionColor[1], i);
            yield return new WaitForEndOfFrame();    
        }
        
    }

    private IEnumerator cFadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        for (float i = 0; i < 1; i += step)
        {
            darkImage.color = Color.Lerp(transictionColor[1], transictionColor[0], i);
            yield return new WaitForEndOfFrame();    
        }
        darkPanel.SetActive(false);
        transiction = false;
    }
}
