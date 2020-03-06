using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public string[] damageType;
    public GameObject[] damageFx;
    public GameObject deathFx;
    public TextMeshProUGUI goldText ;
    private Fade fade;

    public int gold = 0; // Armazena de quantidade de ouro que coletamos
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;
        fade.fadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = gold.ToString("N0", CultureInfo.CurrentCulture);
    }
}
