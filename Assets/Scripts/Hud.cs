using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Hud : MonoBehaviour
{
    private Player player;
    public Image[] hpBar;
    public Sprite half,full;
    private void Start() 
    {
        player = FindObjectOfType(typeof(Player)) as Player;    
    }
}
