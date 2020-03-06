using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform playerTransform;
    public Transform destination;
    private Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact()
    {
        StartCoroutine("triggerDoorAction");
    }

    private IEnumerator triggerDoorAction()
    {
        fade.fadeIn();
        yield return new WaitWhile(() => fade.darkImage.color.a < 0.95f);
        playerTransform.gameObject.SetActive(false);
        playerTransform.position = destination.position;
        yield return new WaitForSeconds(0.5f);
        playerTransform.gameObject.SetActive(true);
        fade.fadeOut();
    }
}
