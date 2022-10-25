using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPos;
    public bool obtainedRangedWeapon;
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
    public SerializableDictionary<string, bool> enemiesDefeated;


    //default values for when the game starts with no data to load
    public GameData()
    {
        this.moonLight = 0;
        this.playerLevel = 1;
        this.vigor = 1;
        this.endurance = 1;
        this.mind = 1;
        this.strength = 1;
        this.dexterity = 1;
        this.faith = 1;
        this.luck = 1;
        playerPos = Vector3.zero;
        obtainedRangedWeapon = false;
        enemiesDefeated = new SerializableDictionary<string, bool>();
    }
}
