using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls all the infos of the player such as hp, stamina, ultCharge, whether they're alive, if they obtained the ranged weapon and more
/// </summary>
public class PlayerInfos : MonoBehaviour, IDataPersistence
{
    #region Variables
    [Header ("Components")]
    PlayerController playerController;
    PlayerCombat playerCombat;
    EnemyInfos enemyInfos;
    [SerializeField] StatBarBehaviour healthBarBehaviour;
    [SerializeField] GameObject companion;

    [Header ("Timer")]
    [SerializeField] float respawnTimer;
    [SerializeField] float respawnTimerInit;
    [SerializeField] public float invincibilityTimer;
    [SerializeField] public float invincibilityTimerInit;

    [Header ("Player Stats")]
    public int playerLevel;
    public float playerMaxHealth;
    public float playerHealth;
    public float staminaRechargeSpeed;
    public float playerMaxStamina;
    public float playerStamina;
    public float dashStaminaRequirement;
    public float playerMaxUltCharg;
    public float playerUltCharge;
    public float playerUltDamage;
    public float playerHealthPercentage;
    public float playerStaminaPercentage;
    public int inventoryState;
    Vector3 startingPos;


    [Header ("Bools")]
    public bool isAlive;
    public bool obtainedRangedWeapon;
    public bool obtainedMoonFragment;
    public bool isDamaged;
    [SerializeField] bool swappedWeapon;
    #endregion


    /// <summary>
    /// grabs references from other scripts
    /// </summary>
    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
    }
    /// <summary>
    /// saves the position the player starts in and sets current hp and stamina to their respective maxAmounts
    /// calculates the percentage of these stats
    /// </summary>
    void Start()
    {
        if(!obtainedMoonFragment)
            playerCombat.enabled = false;
        else
            companion.SetActive(true);

        startingPos = transform.position;
        respawnTimer = respawnTimerInit;
        playerHealth = playerMaxHealth;
        playerStamina = playerMaxStamina;
        invincibilityTimer = invincibilityTimerInit;
        PlayerStatPercentage();
    }

    /// <summary>
    /// loads data that has been saved
    /// loads default data if there's no saveFile
    /// destroys the rangedWeapon/firstMoonFragment in the scene if it was already picked up by the player
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(GameData data)
    {
        this.playerLevel = data.playerLevel;
        this.obtainedRangedWeapon = data.obtainedRangedWeapon;
        this.obtainedMoonFragment = data.obtainedMoonFragment;
        this.transform.position = data.playerPos;
        if(data.obtainedRangedWeapon)
        {
            Destroy(GameObject.FindGameObjectWithTag("WeaponPickup"));
        }

        if (data.obtainedMoonFragment)
        {
            Destroy(GameObject.FindGameObjectWithTag("FirstMoonFragment"));
        }

    }

    /// <summary>
    /// saves the data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        data.playerLevel = this.playerLevel;
        data.obtainedRangedWeapon = this.obtainedRangedWeapon;
        data.obtainedMoonFragment = this.obtainedMoonFragment;
        data.playerPos = this.transform.position;
    }

    /// <summary>
    /// sets bool of whether the player is alive or not
    /// if the player dies the respawn timer gets count down, respawing the player when reaching 0
    /// if the player gets damaged a timer gets started to determine for how long the player is invincible, setting the isDamaged bool to false
    /// if it reached 0
    /// while the players stamina is lower than the staminaMaxAmount, it gets increased by the given values
    /// </summary>
    void Update()
    {
        if(playerHealth > 0)
            isAlive = true;
        if(!isAlive)
        {
            if(respawnTimer > 0)
                respawnTimer -= Time.deltaTime;
            else
                ResetPlayer();
        }
            
        if(invincibilityTimer > 0)
            invincibilityTimer -= Time.deltaTime;
        else
            isDamaged = false;

        if(playerStamina < playerMaxStamina)
            playerStamina += Time.deltaTime * staminaRechargeSpeed;

        PlayerStatPercentage();
    }

    /// <summary>
    /// manages the state of the currently equipped weapon and sets according parameter in animator
    /// </summary>
    public void SwapWeapon()
    {
        if(obtainedRangedWeapon)
            swappedWeapon = !swappedWeapon;

        if(swappedWeapon)
            inventoryState = 1;
        else
            inventoryState = 0;

        playerController.anim.SetFloat("InventoryState", inventoryState);
    }

    /// <summary>
    /// calculates player health with the damage values from the EnemyInfos.cs script
    /// if the player has less than 1 hp they're not alive anymore (duh...)
    /// </summary>
    /// <param name="dmg">passed in from the enemy the player touched</param>
    public void CalculatePlayerHealth(float dmg) 
    {
        if(isDamaged)
            playerHealth -= dmg;
        if(playerHealth < 1)
            isAlive = false;

        healthBarBehaviour.FadingBarBehaviour();
    }

    /// <summary>
    /// gives back a value from 0-1 for the player Health, Stamina and UltCharge
    /// </summary>
    void PlayerStatPercentage()
    {
        playerHealthPercentage = playerHealth / playerMaxHealth;
        playerStaminaPercentage = playerStamina / playerMaxStamina;
    }
    
    /// <summary>
    /// resets playerValues to the values that were saved beforehand
    /// </summary>
    void ResetPlayer()
    {
        playerHealth = playerMaxHealth;
        transform.position = startingPos;
        respawnTimer = respawnTimerInit;
        PlayerStatPercentage();
        healthBarBehaviour.SetStat(playerHealthPercentage);
    }

    public void ObtainedFirstMoonFragment()
    {
        obtainedMoonFragment = true;
        playerCombat.enabled = true;
    }
}
