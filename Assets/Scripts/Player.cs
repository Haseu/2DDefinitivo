using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    public Transform groundCheck;   // objeto responsável por detectar se o personagem está sobre uma superfície
    public float speed;             // Velocidade de movimento do personagem
    public float jumpForce;         // Força aplicada para gerar o pulo do personagem
    public bool grounded;           // Indica se o personagem está pisando em alguma superfície
    public bool mirrored;           //Indica se o personagem está virado para esquerda
    public int idAnimation;         // Indica id da animação

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h > 0 && mirrored)
        {
            this.flip();
        }
        else if (h < 0 &&  !mirrored)
        {
            this.flip();
        }

        if (v < 0)
        {
            idAnimation = 2;
        }
        else if (h != 0)
        {
            idAnimation = 1;
        }
        else
        {
            idAnimation = 0;
        }

        if(Input.GetButtonDown("Fire1") && v >= 0)
        {
            playerAnimator.SetTrigger("attack");
        }

        if(Input.GetButtonDown("Jump") && grounded)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);

        playerAnimator.SetBool("grounded", grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
    }

    private void FixedUpdate() 
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);    
    }

    private void flip()
    {
        this.mirrored = !mirrored;
        float x = transform.localScale.x;
        x *= -1; //inverte o sinal do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
