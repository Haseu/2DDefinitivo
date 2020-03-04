using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControll : MonoBehaviour
{
    private GameController gameController;
    public float[] resistDamage; // Sistema de resistencia / fraquesa contra determinado tipo de dano

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.tag)
        {
            case  "Weapon":

            WeaponInfo weaponInfo = other.gameObject.GetComponent<WeaponInfo>();
            
            float weaponDamage = weaponInfo.damage;
            int damageType = weaponInfo.typeDamage;

            float damage = weaponDamage + (weaponDamage * (resistDamage[damageType] / 100));

            print("tomou "+damage+" de dano do tipo " + gameController.damageType[damageType]);
            break;
        }    
    }
}
