using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelBehaviour : MonoBehaviour, IDataPersistence
{
    #region Variables
    [Header ("Main Components")]
    PlayerController playerController;
    PlayerCombat playerCombat;
    PlayerMeleeWeaponBehaviour playerMeleeWeaponBehaviour;
    PlayerInfos playerInfos;
    EnemyInfos enemyInfos;

    [Header ("Level Up Values")]
    [SerializeField] int collectedMoonFragments;
    [SerializeField] public float moonLight;
    [SerializeField] public float requiredMoonLight;
    [SerializeField] float playerHealthIncrease;
    [SerializeField] float staminaIncrease;
    [SerializeField] public bool levelUpReady;
    #endregion
    public int vigor;
    public int endurance;
    public int mind;
    public int strength;
    public int dexterity;
    public int faith;
    public int luck;

    #region Stats to level
    // - Vigor -> Overall HP
    // - Endurance -> Overall stamina
    // - Mind -> Execute @ specific hp% || bleed build-up
    // - Strength -> do stuff but heavy
    // - Dexterity -> do stuff but fast
    // - Faith -> radiant/burning damage
    // - Luck -> crit damage
    #endregion

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerMeleeWeaponBehaviour = gameObject.GetComponentInChildren<PlayerMeleeWeaponBehaviour>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
    }

    void Start()
    {
        RequiredRunes(playerInfos.playerLevel);
    }

    public void LoadData(GameData data)
    {
        this.moonLight = data.moonLight;

        this.vigor = data.vigor;
        this.endurance = data.endurance;
        this.mind = data.mind;
        this.strength = data.strength;
        this.dexterity = data.dexterity;
        this.faith = data.faith;
        this.luck = data.luck;

    }

    public void SaveData(ref GameData data)
    {
        data.moonLight = this.moonLight;

        data.vigor = this.vigor;
        data.endurance = this.endurance;
        data.mind = this.mind;
        data.strength = this.strength;
        data.dexterity = this.dexterity;
        data.faith = this.faith;
        data.luck = this.luck;
    }
    void Update()
    {
        if(moonLight >= requiredMoonLight) 
            levelUpReady = true;
        else
            levelUpReady = false;
    }

    //removes the needed amount of runes from the player, increases playerLevel by 1
    public void LevelUp()
    {
        moonLight -= requiredMoonLight; 
        playerInfos.playerLevel += 1;
        playerMeleeWeaponBehaviour.DetermineAllTheDamages();
    }

    //calculates the needed amount of runes based on the formula
    // y = 0.02x³ + 3.06x² + 105.6x - 895 => y = requiredMoonLight, x = playerLevel + 1
    //idk if the conversion is right, but the returned values make sense
    public void RequiredRunes(int _playerLevel)
    {
        LevelUp();
        _playerLevel += 1;
        requiredMoonLight = _playerLevel * (_playerLevel * (0.02f * _playerLevel + 3.06f)) + 105.6f * _playerLevel; 
        requiredMoonLight = Mathf.Floor(requiredMoonLight);
        Debug.Log(requiredMoonLight);
    }

    #region Stat increases
    
    //increases the playerHealth by 25-45HP
    public void IncreasePlayerHP()
    {
        vigor += 1;
        int rnd = Random.Range(25, 45);
        playerInfos.playerMaxHealth += rnd;
        playerInfos.playerHealth += rnd;
    }

    //increases the playerStamina by 1-3
    public void IncreasePlayerEndurance()
    {
        endurance  += 1;
        int rnd = Random.Range(1, 3);
        playerInfos.playerMaxStamina += rnd;
        playerInfos.playerStamina += rnd;
    }

    //DoT || execute @ specific hp%
    public void IncreasePlayerMind()
    {
        mind  += 1;
    }

    //increases damage dealt by the player
    public void IncreasePlayerStrength()
    {
        strength  += 1;
    }

    //increases playerMovementspeed and invincibility timer
    public void IncreasePlayerDexterity()
    {
        dexterity  += 1;
        playerController.maxSpeed += .2f;
    }

    //burning/radiant damage
    public void IncreasePlayerFaith()
    {
        faith  += 1;
    }

    //increases crit chance
    public void IncreasePlayerLuck()
    {
        luck  += 1;
    }
    #endregion
}
