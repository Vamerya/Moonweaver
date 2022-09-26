using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfos : MonoBehaviour
{
    public Vector3 runesDamageHP;
    void Start()
    {
    
    }

    void Update()
    {
        
    }

    public Vector3 DetermineEnemyType(int ID)
    {
        switch(ID)
        {
            case 0:
                runesDamageHP = new Vector3(500, 100, 1000); //normal melee add
            break; 
            case 1:
                runesDamageHP = new Vector3(500, 200, 500); //normal ranged add
            break; 
            case 2:
                runesDamageHP = new Vector3(1000, 100, 1500); //tanky enemy
            break; 
            case 3:
                runesDamageHP = new Vector3(1500, 1000, 500); //assassin
            break; 
            case 4:
                runesDamageHP = new Vector3(500, 100, 1000); //open slot
            break; 
            case 5:
                runesDamageHP = new Vector3(500, 100, 1000); //open slot
            break; 
            default:
                runesDamageHP = new Vector3(10000, 1000, 5000); //Boss type
            break; 

        }

        return runesDamageHP;
    }
}
