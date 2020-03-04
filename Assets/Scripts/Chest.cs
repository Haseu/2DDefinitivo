using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] imageObject;
    public bool open;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>() as GameController;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void interact()
    {
        if(!open)
        {
            open = true;
            spriteRenderer.sprite = imageObject[1];
            gameController.teste += 1;
        }    
    }
}
