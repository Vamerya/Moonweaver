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
        //ToDo - think of better formula for weapon damage
        playerWeaponDamage = playerLevelBehaviour.strength * 50f;
        playerWeaponDamage = Mathf.RoundToInt(playerWeaponDamage); 
    }
}
