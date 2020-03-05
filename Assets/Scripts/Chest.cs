using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] imageObject;
    public bool open;
    public GameObject[] loot;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void interact()
    {
        if(!open)
        {
            open = true;
            spriteRenderer.sprite = imageObject[1];
            
            StartCoroutine(this.spawnLoot());
            GetComponent<Collider2D>().enabled = false;
        }    
    }

    IEnumerator spawnLoot()
    {
         //Controle de spawn de loot
            int qtdCoins = Random.Range(1,5);

            for (int i = 0; i < qtdCoins; i++)
            {
                int rand = Random.Range(0,2);
                GameObject lootTemp = Instantiate(loot[rand], transform.position, transform.localRotation);
                lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 200));   
                yield return new WaitForSeconds(0.1f);
            }
    }
}
