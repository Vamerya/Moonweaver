using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPos;
    public Vector3 respawnPos;
    public int moonwater;
    public int moonFragments;
    public float storedMoonFragments;
    public bool obtainedRangedWeapon;
    public bool obtainedMoonFragment;
    public float playerHealth;
    public float playerStamina;
    #region Stats
    public float moonLight;
    public int playerLevel;
    public int vigor;
    public int endurance;
    public int mind;
    public int strength;
    public int dexterity;
    public int faith;
    public int luck;
    #endregion 
    public SerializableDictionary<string, bool> bossesDefeated;


    /// <summary>
    /// default values for when the game starts with no data to load
    /// </summary>
    public GameData()
    {
        this.moonLight = 0;
        this.storedMoonFragments = 0;
        this.playerLevel = 1;
        this.playerHealth = 400;
        this.playerStamina = 150;
        this.vigor = 1;
        this.endurance = 1;
        this.mind = 1;
        this.strength = 1;
        this.dexterity = 1;
        this.faith = 1;
        this.luck = 1;
        this.moonwater = 3;
        this.moonFragments = 0;
        playerPos = Vector3.zero;
        respawnPos = Vector3.zero;
        obtainedRangedWeapon = false;
        obtainedMoonFragment = false;
        bossesDefeated = new SerializableDictionary<string, bool>();
    }
}
