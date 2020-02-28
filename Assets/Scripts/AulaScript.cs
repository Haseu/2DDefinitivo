using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AulaScript : MonoBehaviour
{
    private Animator playerAnimator;
    public bool grounded;
    public int idAnimation;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

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

        playerAnimator.SetBool("grounded", grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
    }
}
