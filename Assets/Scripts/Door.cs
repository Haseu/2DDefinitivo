using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Player player;
    public Transform destination;
    private Fade fade;
    public bool dark;
    public Material light2D, standard2D;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;    
        player = FindObjectOfType(typeof(Player)) as Player;    
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
        player.gameObject.SetActive(false);

        switch (dark)
        {
            case true:
                player.changeMaterial(light2D);
                break;
            case false:
                player.changeMaterial(standard2D); ;
                break;
        }

        player.transform.position = destination.position;
        player.gameObject.SetActive(true);
        fade.fadeOut();
        
    }
}
