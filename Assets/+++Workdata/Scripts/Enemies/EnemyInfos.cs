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
                runesDamageHP = new Vector3(5, 1, 10); //normal melee add
            break; 
            case 1:
                runesDamageHP = new Vector3(5, 2, 5); //normal ranged add
            break; 
            case 2:
                runesDamageHP = new Vector3(10, 1, 15); //tanky enemy
            break; 
            case 3:
                runesDamageHP = new Vector3(15, 10, 5); //assassin
            break; 
            case 4:
                runesDamageHP = new Vector3(5, 1, 10); //open slot
            break; 
            case 5:
                runesDamageHP = new Vector3(5, 1, 10); //open slot
            break; 
            default:
                runesDamageHP = new Vector3(100, 10, 50); //Boss type
            break; 

        }

        return runesDamageHP;
    }
}
