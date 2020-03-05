using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController gameController;
    public int value;
    
    private void Start() {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    public void collect()
    {
        gameController.gold += value;
        Destroy(this.gameObject);
    }
}
