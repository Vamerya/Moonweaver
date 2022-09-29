using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelBehaviour : MonoBehaviour
{
    #region Variables
    [Header ("Main Components")]
    PlayerController playerController;
    PlayerCombat playerCombat;
    PlayerInfos playerInfos;
    EnemyInfos enemyInfos;

    [Header ("Level Up Values")]
    [SerializeField] int collectedMoonFragments;
    [SerializeField] public float runes;
    [SerializeField] public float requiredRunes;
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
    // - Mind -> Ult Recharge
    // - Strength -> Overall damage
    // - Dexterity -> Invincibility time
    // - Faith -> Increased spell damage (ult for now)
    // - Luck -> increased item drop chance/runes
    #endregion

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
    }

    void Start()
    {
        RequiredRunes(playerInfos.playerLevel);
    }

    void Update()
    {
        if(runes >= requiredRunes) 
            levelUpReady = true;
        else
            levelUpReady = false;
    }

    //removes the needed amount of runes from the player, increases playerLevel by 1
    public void LevelUp()
    {
       runes -= requiredRunes; 
       playerInfos.playerLevel += 1;
    }

    //calculates the needed amount of runes based on the formula
    // y = 0.02x³ + 3.06x² + 105.6x - 895 => y = requiredRunes, x = playerLevel + 1
    //idk if the conversion is right, but the returned values make sense
    public void RequiredRunes(int _playerLevel)
    {
        LevelUp();
        _playerLevel += 1;
        requiredRunes = _playerLevel * (_playerLevel * (0.02f * _playerLevel + 3.06f)) + 105.6f * _playerLevel; 
        requiredRunes = Mathf.Floor(requiredRunes);
        Debug.Log(requiredRunes);
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

    //increases the amount of charge per hit for the playerUltimate
    public void IncreasePlayerMind()
    {
        mind  += 1;
        //Increase Ult charge gained per hit
    }

    //increases damage dealt by the player
    public void IncreasePlayerStrength()
    {
        strength  += 1;
        //increase damage of melee and ranged weapon
    }

    //increases playerMovementspeed and invincibility timer
    public void IncreasePlayerDexterity()
    {
        dexterity  += 1;
        playerInfos.invincibilityTimerInit += .1f;
        playerController.maxSpeed += .2f;
    }

    //increases the damage of the ultimate
    public void IncreasePlayerFaith()
    {
        faith  += 1;
        playerInfos.playerUltDamage += 50;
        //increase spell damage
    }

    //increases the drop amount of runes and items by enemies
    public void IncreasePlayerLuck()
    {
        luck  += 1;
        enemyInfos.runesDamageHP.x *= 1.2f;
        enemyInfos.runesDamageHP.x = Mathf.Floor(enemyInfos.runesDamageHP.x);
        //dropchance increase
    }
    #endregion
}
