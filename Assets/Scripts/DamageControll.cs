using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControll : MonoBehaviour
{
    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    [Header("Configuração de Vida")]
    public int life;
    public int actualLife;
    public GameObject lifeBar; //Objeto contendo todas as barras
    public Transform hpBar; //Objeto indicador da quantidade de vida
    public Color[] characterColor; //Controle de cores do personagem
    private float percentLife;
    public GameObject damageTextPrefab; //Exibe a quantidade de dano recebido
    
    [Header("Configuração de Resistência/Fraqueza")]
    public float[] resistDamage; // Sistema de resistencia / fraquesa contra determinado tipo de dano

    [Header("Configuração do Knockback")]
    public GameObject knockForcePrefab; //Força de repulsão
    public Transform knockPosition; //Força de origem da força
    private Player player;
    public bool mirrored;  
    private bool playerPositionLeft;
    public float knockX;
    private float kx;
    private bool getHited;


    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        player = FindObjectOfType(typeof(Player)) as Player;
        spriteRenderer = GetComponent<SpriteRenderer>();
        lifeBar.SetActive(false);
        actualLife = life;
        hpBar.localScale = new Vector3(1, 1, 1); // precisa?

        spriteRenderer.color = characterColor[0];
       
        if(mirrored)
        {
            float x = transform.localScale.x;
            x *= -1; //inverte o sinal do scale x
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
            lifeBar.transform.localScale = new Vector3(x, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica o posicionamento do player em relação ao inimigo
        float xPlayer = player.transform.position.x;

        if(xPlayer < transform.position.x)
        {
            playerPositionLeft = true;
        }
        else if(xPlayer > transform.position.x)
        {
            playerPositionLeft = false;
        }

        if(mirrored && playerPositionLeft)
        {
            kx = knockX; 
        }
        else if (!mirrored && playerPositionLeft)
        {
            kx = knockX * -1;
        }
        else if (mirrored && !playerPositionLeft)
        {
            kx = knockX * -1;
        }
        else if (!mirrored && !playerPositionLeft)
        {
            kx = knockX;
        }

        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);



    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.tag)
        {
            case  "Weapon":

            if(!getHited)
            {
                lifeBar.SetActive(true);
                getHited = true;
                WeaponInfo weaponInfo = other.gameObject.GetComponent<WeaponInfo>();
                
                float weaponDamage = weaponInfo.damage;
                int damageType = weaponInfo.typeDamage;

                int damage = Mathf.RoundToInt(weaponDamage + (weaponDamage * (resistDamage[damageType] / 100)));

                actualLife -= damage; // Reduz da quantidade de vida

                percentLife = (float) actualLife / (float) life;

                if(percentLife < 0)
                {
                    percentLife = 0;
                }

                hpBar.localScale = new Vector3(percentLife, 1, 1);

                if(actualLife <= 0)
                {
                    Destroy(this.gameObject);
                }

                print("tomou "+actualLife+" de dano do tipo " + gameController.damageType[damageType]);

                GameObject damageTextTemp = Instantiate(damageTextPrefab, transform.position, transform.rotation);
                damageTextTemp.GetComponent<TextMesh>().text = damage.ToString();
                damageTextTemp.GetComponent<MeshRenderer>().sortingLayerName = "HUD";

                int forceX = 50;
                if(player.mirrored)
                {
                    forceX *= -1;
                }

                damageTextTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 230));
                Destroy(damageTextTemp, 1f);

                GameObject knockTemp = Instantiate(knockForcePrefab, knockPosition.position, knockPosition.localRotation);
                Destroy(knockTemp, 0.02f);

                StartCoroutine(this.invunerable());
            }
            break;
        }    
    }

    private void flip()
    {
        this.mirrored = !mirrored;
        float x = transform.localScale.x;
        x *= -1; //inverte o sinal do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        lifeBar.transform.localScale = new Vector3(x, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        //direction.x = x;
    }

    private IEnumerator invunerable()
    {
        spriteRenderer.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = characterColor[0];
        getHited = false;
        lifeBar.SetActive(false);
    }
}
