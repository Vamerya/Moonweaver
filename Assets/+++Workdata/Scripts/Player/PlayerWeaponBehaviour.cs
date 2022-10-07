using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBehaviour : MonoBehaviour
{
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    public float playerWeaponDamage;

    void Start()
    {
        DetermineWeaponDamage();
    }

    public void DetermineWeaponDamage()
    {
        playerWeaponDamage = playerLevelBehaviour.strength * 50f;
        playerWeaponDamage = Mathf.RoundToInt(playerWeaponDamage); 
    }
}
