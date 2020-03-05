using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private Player player;
    [SerializeField]
    public Image[] hpBar;
    public Sprite half,full;

    private void Start() 
    {
        player = FindObjectOfType(typeof(Player)) as Player;    
    }

    private void Update() 
    {
        this.lifeBarControl();    
    }

    private void lifeBarControl()
    {
        float percentualLife = (float) player.actualLife / (float) player.life; //Calcula percentual de vida 0 - 1

        if (percentualLife == 1)
        {
            //Representa 100% de vida
            foreach (Image img in hpBar)
            {
                img.enabled = true;
                img.sprite = full;
            }
        }
        else if (percentualLife >= 0.9f)
        {
            hpBar[4].sprite = half;
        }
        else if (percentualLife >= 0.8f)
        {
            hpBar[4].enabled = false;
        }
        else if (percentualLife >= 0.7f)
        {
            hpBar[4].enabled = false;
            hpBar[3].sprite = half;
        }
        else if (percentualLife >= 0.6f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
        }
        else if (percentualLife >= 0.5f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].sprite = half;
        }
        else if (percentualLife >= 0.4f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
        }
        else if (percentualLife >= 0.3f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].sprite = half;
        }
        else if (percentualLife >= 0.2f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;
        }
        else if (percentualLife >= 0.01f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;
            hpBar[0].sprite = half;
        }
        else if (percentualLife <= 0f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;
            hpBar[0].enabled = false;
        }


    }
}
