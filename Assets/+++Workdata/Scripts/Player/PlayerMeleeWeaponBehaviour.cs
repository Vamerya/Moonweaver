using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] public float playerWeaponDamage;
    [SerializeField] float playerCriticalStrikeChance;
    [SerializeField] float playerCriticalStrikeDamage;
    [SerializeField] public float playerBurnDamage;
    float _playbackTime;

    #region stats and what they do
    // - Vigor -> Overall HP
    // - Endurance -> Overall stamina
    // - Mind -> Execute @ specific hp% && crit chance
    // - Strength -> do stuff but heavy
    // - Dexterity -> do stuff but fast || increase ranged weapon damage
    // - Faith -> burning damage
    // - Luck -> crit damage
    #endregion

    void Start()
    {

    }

    void Update()
    {
        DetermineAllTheDamages();
    }

    public void DetermineAllTheDamages()
    {
        DetermineDexterityWeaponValues();
        DetermineStrengthWeaponValues();
        DetermineBurningDamage();
        DetermineCriticalStrikeChance();
        DetermineCriticalStrikeDamage();
    }


/// <summary>
/// 
/// </summary>
/// <returns></returns>
    public float DetermineWeaponDamage()
    {
        playerWeaponDamage = Mathf.Log(playerLevelBehaviour.strength, 5) * 50;
        playerWeaponDamage = Mathf.Floor(playerWeaponDamage);

        return playerWeaponDamage;
    }
    public void DetermineDexterityWeaponValues()
    {
        // ToDo - increase attackSpeed
    }

    public void DetermineStrengthWeaponValues()
    {
        //decreases heavy charge timer
        playerCombat.chargingTimerGoal = (Mathf.Log(playerLevelBehaviour.strength, 5) / -1) + 3;
    }

    public float DetermineBurningDamage()
    {
        playerBurnDamage = Mathf.Log(playerLevelBehaviour.faith, 2) * 2.5f;
        playerBurnDamage = Mathf.Floor(playerBurnDamage);
        return playerBurnDamage;
    }

    public void DetermineCriticalStrikeChance()
    {
        playerCriticalStrikeChance = Mathf.Log(playerLevelBehaviour.luck, 5);
        playerCriticalStrikeChance /= 10;
    }

    public float DetermineCriticalStrikeDamage()
    {
        playerCriticalStrikeDamage = playerWeaponDamage * (2 + (playerLevelBehaviour.mind / 100));
        return playerCriticalStrikeDamage;
    }

    public void ExecuteEnemy() //or maybe big damage after a specific amount of buildup is reached (Dark Souls/Elden Ring bleed)
    {
        //ToDo - add execute
    }

    public float DamageEnemyMelee()
    {
        if(Random.value < playerCriticalStrikeChance)
        {
            DetermineCriticalStrikeDamage();
            playerCriticalStrikeDamage = Mathf.Floor(playerCriticalStrikeDamage);
            Debug.Log("crit");
            return playerCriticalStrikeDamage;
        }
        else
        {
            DetermineWeaponDamage();
            playerWeaponDamage = Mathf.Floor(playerWeaponDamage);
            Debug.Log("no crit");
            return playerWeaponDamage;
        }
    }
}
