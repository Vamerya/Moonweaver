using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfos : MonoBehaviour
{
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    public Vector3 moonLightDamageHP;
    void Start()
    {
    
    }

    void Update()
    {
        if(moonLightDamageHP.z < 1)
        {
            AddMoonLight();
            Destroy(gameObject);
        }
    }

    public Vector3 DetermineEnemyType(int ID)
    {
        switch(ID)
        {
            case 0:
                moonLightDamageHP = new Vector3(Random.Range(400, 600), Random.Range(80, 120), 1000); //normal melee add
            break; 
            case 1:
                moonLightDamageHP = new Vector3(Random.Range(400, 600), Random.Range(180, 220), 500); //normal ranged add
            break; 
            case 2:
                moonLightDamageHP = new Vector3(Random.Range(800, 1200), Random.Range(80, 120), 1500); //tanky enemy
            break; 
            case 3:
                moonLightDamageHP = new Vector3(Random.Range(1300, 1700), Random.Range(800, 1200), 500); //assassin
            break; 
            case 4:
                moonLightDamageHP = new Vector3(500, 100, 1000); //open slot
            break; 
            case 5:
                moonLightDamageHP = new Vector3(500, 100, 1000); //open slot
            break; 
            default:
                moonLightDamageHP = new Vector3(10000, 1000, 5000); //Boss type
            break; 

        }

        return moonLightDamageHP;
    }

    void EnemyTakeDamage(float dmg)
    {
        moonLightDamageHP.z -= dmg;
    }

    void AddMoonLight()
    {
        playerLevelBehaviour.moonLight += moonLightDamageHP.x; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            EnemyTakeDamage(collision.GetComponent<PlayerWeaponBehaviour>().playerWeaponDamage);
        }
    }
}
