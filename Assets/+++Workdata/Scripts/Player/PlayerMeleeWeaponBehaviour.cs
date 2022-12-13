using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the overall behaviour of the players melee weapon
/// </summary>
public class PlayerMeleeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] public float playerWeaponDamage;
    [SerializeField] float playerCriticalStrikeChance;
    [SerializeField] float playerCriticalStrikeDamage;
    [SerializeField] public float playerBurnDamage;

    #region stats and what they do
    // - Vigor -> Overall HP
    // - Endurance -> Overall stamina
    // - Mind -> criticalStrikeChance
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
    /// determines the players melee weapon damage based on their level of strength put into a logarithmic function with some funky numbers
    /// that just work
    /// </summary>
    /// <returns>playerWeaponDamage</returns>
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

    /// <summary>
    /// decreases the amount of time needed to charge a heavy attack based on the level of strengt the player has
    /// </summary>
    public void DetermineStrengthWeaponValues()
    {
        //decreases heavy charge timer
        playerCombat.chargingTimerGoal = (Mathf.Log(playerLevelBehaviour.strength, 5) / -1) + 3;
    }

    /// <summary>
    /// determines how much damage each tick of burning damage should do
    /// </summary>
    /// <returns>burnDamage per tick</returns>
    public float DetermineBurningDamage()
    {
        playerBurnDamage = Mathf.Log(playerLevelBehaviour.faith, 2) * 2.5f;
        playerBurnDamage = Mathf.Floor(playerBurnDamage);
        return playerBurnDamage;
    }

    /// <summary>
    /// determines the chance the player lands a critical strike based on their level of luck
    /// </summary>
    public void DetermineCriticalStrikeChance()
    {
        playerCriticalStrikeChance = Mathf.Log(playerLevelBehaviour.luck, 5);
        playerCriticalStrikeChance /= 10;
    }

    /// <summary>
    /// determines how much damage a critical strike should deal based on the normal melee damage
    /// </summary>
    /// <returns>damage when the player lands a critical strike</returns>
    public float DetermineCriticalStrikeDamage()
    {
        playerCriticalStrikeDamage = playerWeaponDamage * (2 + (playerLevelBehaviour.mind / 10));
        return playerCriticalStrikeDamage;
    }

    public void ExecuteEnemy() //or maybe big damage after a specific amount of buildup is reached (Dark Souls/Elden Ring bleed)
    {
        //ToDo - add execute
    }

    /// <summary>
    /// Method that gets used to damage enemiese
    /// if the random value is smaller than the criticalStrikeChance the critDamage gets applied instead of the normal damage
    /// if the random value is bigger than the criticalStrikeChance the normal amount of damage gets applied
    /// </summary>
    /// <returns>amount of damaged based on if the playere landed a critical strike or not</returns>
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
