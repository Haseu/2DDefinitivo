using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController gameController;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    public Transform groundCheck;   // objeto responsável por detectar se o personagem está sobre uma superfície
    public LayerMask isGround;      // Indica se a superficie para teste do grounded
    public float speed;             // Velocidade de movimento do personagem
    public float jumpForce;         // Força aplicada para gerar o pulo do personagem
    public bool grounded;           // Indica se o personagem está pisando em alguma superfície
    public bool isAttacking;          // Indica se o personagem está executando um ataque
    public bool mirrored;           //Indica se o personagem está virado para esquerda
    public int idAnimation;         // Indica id da animação
    private float h, v;
    public BoxCollider2D standingCollider, crounchCollider;
    public int life;
    public int actualLife;

    //Interação com items e objetos
    private Vector3 direction = Vector3.right;
    public Transform hand;
    public LayerMask interaction;
    private GameObject objetcInteraction;

    //Sistema de Armas
    public GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    { 
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        this.actualLife = this.life;

        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h > 0 && mirrored && !isAttacking)
        {
            this.flip();
        }
        else if (h < 0 &&  !mirrored && !isAttacking)
        {
            this.flip();
        }

        if (v < 0)
        {
            idAnimation = 2;
            if(grounded){
                h = 0;
            }            
        }
        else if (h != 0)
        {
            idAnimation = 1;
        }
        else
        {
            idAnimation = 0;
        }

        if(Input.GetButtonDown("Fire1") && v >= 0 && !isAttacking && objetcInteraction == null)
        {
            playerAnimator.SetTrigger("attack");
        }

        if(Input.GetButtonDown("Fire1") && v >= 0 && !isAttacking && objetcInteraction != null)
        {
            objetcInteraction.SendMessage("interact", SendMessageOptions.DontRequireReceiver);
        }

        if(Input.GetButtonDown("Jump") && grounded && !isAttacking)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        if (isAttacking && grounded)
        {
            this.h = 0;
        }

        if (v < 0 && grounded)
        {
            crounchCollider.enabled = true;
            standingCollider.enabled = false;
        }
        else if ((v >= 0 && grounded) || (v != 0 && !grounded))
        {
            crounchCollider.enabled = false;
            standingCollider.enabled = true;
        }

        playerAnimator.SetBool("grounded", grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
    }

    private void FixedUpdate() //Taxa de atualização fixa 0.02
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, isGround);    //Teste colisão com chão
        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);
        this.interact();
    }

    private void flip()
    {
        this.mirrored = !mirrored;
        float x = transform.localScale.x;
        x *= -1; //inverte o sinal do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        direction.x = x;
    }

    public void checkIsAttacking(int isAttacking)
    {  
        switch (isAttacking)
        {
            case 0:
                this.isAttacking = false;
                weapons[2].SetActive(false);
                break;
            case 1:
                this.isAttacking = true;
                break;
        }
    }

    private void interact()
    {
        Debug.DrawRay(hand.position, direction * 0.08f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(hand.position, direction, 0.08f, interaction);

        if(hit)
        {
            objetcInteraction = hit.collider.gameObject;
        }
        else
        {
            objetcInteraction = null;
        }
    }

    //Metodos para tratar colisões
    private void OnCollisionEnter2D(Collision2D other) //Ao Colidir
    { 
        if(other.gameObject.tag == "Caixa")
        {

        }
        switch (other.gameObject.tag)
        {
            case "Collectable":
            other.gameObject.SendMessage("collect", SendMessageOptions.DontRequireReceiver);
            break;
        }
    }

    private void weaponControll(int id)
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        weapons[id].SetActive(true);
    }


    private void OnCollisionExit2D(Collision2D other) 
    {
        
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        
    }
}
