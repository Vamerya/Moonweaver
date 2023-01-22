using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the players level and calculates the amount of Moonlight (currency) needed for the next level-up
/// </summary>
public class PlayerLevelBehaviour : MonoBehaviour, IDataPersistence
{
    #region Variables
    [Header ("Main Components")]
    PlayerController playerController;
    PlayerCombat playerCombat;
    PlayerMeleeWeaponBehaviour playerMeleeWeaponBehaviour;
    PlayerInfos playerInfos;

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

    /// <summary>
    /// grabs references to necessary scripts of the player
    /// </summary>
    void Awake()
    {
        playerMeleeWeaponBehaviour = gameObject.GetComponentInChildren<PlayerMeleeWeaponBehaviour>();
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    /// <summary>
    /// determines the required moonlight based on the players level at the start of the game
    /// </summary>
    void Start()
    {
        RequiredMoonlight(playerInfos.playerLevel);
    }

    /// <summary>
    /// loads data based on what has been saved
    /// loads default data if none has been saved beforehand
    /// </summary>
    /// <param name="data"></param>
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

    /// <summary>
    /// saves the data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
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

    /// <summary>
    /// checks whether the player has enough Moonlight to level up
    /// </summary>
    void Update()
    {
        if(moonLight >= requiredMoonLight)
            levelUpReady = true;
        else
            levelUpReady = false;
    }

    /// <summary>
    /// removes the needed amount of Moonlight from the player, increases playerLevel by 1
    /// </summary>
    public void LevelUp()
    {
        moonLight -= requiredMoonLight; 
        playerInfos.playerLevel += 1;
        playerMeleeWeaponBehaviour.DetermineAllTheDamages();
    }

    /// <summary>
    /// used for the initial calculation of the level up cost based on the players level witout initializing a level up/increasing 
    /// the level of the player
    /// </summary>
    /// <param name="_playerLevel">level of the player</param>
    public void RequiredMoonlight(int _playerLevel)
    {
        requiredMoonLight = _playerLevel * (_playerLevel * (0.02f * _playerLevel + 3.06f)) + 105.6f * _playerLevel;
        requiredMoonLight = Mathf.Floor(requiredMoonLight);
    }

    /// <summary>
    /// calculates the needed amount of Moonlight based on the formula and the players overall level
    /// y = 0.02x³ + 3.06x² + 105.6x - 895 => y = requiredMoonLight, x = playerLevel + 1
    /// idk if the conversion is right, but the returned values make sense
    /// </summary>
    /// <param name="_playerLevel">playerlevel to determine the amount of Moonlight needed</param>
    public void RequiredMoonlightAfterLevelUp(int _playerLevel)
    {
        LevelUp();
        _playerLevel += 1;
        requiredMoonLight = _playerLevel * (_playerLevel * (0.02f * _playerLevel + 3.06f)) + 105.6f * _playerLevel; 
        requiredMoonLight = Mathf.Floor(requiredMoonLight);
    }

    #region Stat increases

    /// <summary>
    /// increases the playerHealth by 25-45HP
    /// </summary>
    public void IncreasePlayerHP()
    {
        vigor += 1;
        int rnd = Random.Range(25, 45);
        playerInfos.playerMaxHealth += rnd;
        playerInfos.playerHealth += rnd;
    }

    /// <summary>
    /// increases the playerStamina by 1-3
    /// </summary>
    public void IncreasePlayerEndurance()
    {
        endurance  += 1;
        int rnd = Random.Range(1, 3);
        playerInfos.playerMaxStamina += rnd;
        playerInfos.playerStamina += rnd;
    }

    /// <summary>
    /// the total level of this stat is used to increase the playeres criticalStrikeDamage
    /// </summary>
    public void IncreasePlayerMind()
    {
        mind  += 1;
    }

    /// <summary>
    /// the total level of this stat is used to increase the players overall melee damage
    /// </summary>
    public void IncreasePlayerStrength()
    {
        strength  += 1;
    }

    /// <summary>
    /// the total level of this stat is used to increase the players overall ranged damage
    /// the player gains .2 movementSpeed per level of DEX
    /// </summary>
    public void IncreasePlayerDexterity()
    {
        dexterity  += 1;
        playerController.maxSpeed += .2f;
    }

    /// <summary>
    /// the total level of this stat is used to increase the burn damage of the player when they hit an enemy
    /// </summary>
    public void IncreasePlayerFaith()
    {
        faith  += 1;
    }

    /// <summary>
    /// the total level of this stat is used to increase the players criticalStrikeChance
    /// </summary>
    public void IncreasePlayerLuck()
    {
        luck  += 1;
    }
    #endregion
}
