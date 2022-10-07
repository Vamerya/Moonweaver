using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    #region Variables
    [Header ("Components")]
    PlayerController playerController;
    PlayerCombat playerCombat;
    EnemyInfos enemyInfos;
    [SerializeField] StatBarBehaviour healthBarBehaviour;
    Vector3 startingPos;

    [Header ("Timer")]
    [SerializeField] float respawnTimer;
    [SerializeField] float respawnTimerInit;
    [SerializeField] public float invincibilityTimer;
    [SerializeField] public float invincibilityTimerInit;

    [Header ("Player Stats")]
    [SerializeField] public int playerLevel;
    [SerializeField] public float playerMaxHealth;
    [SerializeField] public float playerHealth;
    [SerializeField] float staminaRechargeSpeed;
    [SerializeField] public float playerMaxStamina;
    [SerializeField] public float playerStamina;
    [SerializeField] public float dashStaminaRequirement;
    [SerializeField] public float playerMaxUltCharg;
    [SerializeField] public float playerUltCharge;
    [SerializeField] public float playerStrength;
    [SerializeField] public float playerDexterity;
    [SerializeField] public float playerUltDamage;
    [SerializeField] public float playerFaith;
    [SerializeField] public float playerLuck;
    [SerializeField] public float playerHealthPercentage;
    [SerializeField] public float playerStaminaPercentage;
    public int inventoryState;

    [Header ("Bools")]
    public bool isAlive;
    public bool obtainedRangedWeapon;
    [SerializeField] public bool isDamaged;
    [SerializeField] bool swappedWeapon;
    #endregion

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();

        startingPos = transform.position;
        respawnTimer = respawnTimerInit;
        playerHealth = playerMaxHealth;
        playerStamina = playerMaxStamina;
        invincibilityTimer = invincibilityTimerInit;

        PlayerStatPercentage();
    }

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

    //manages the state of the currently equipped weapon 
    public void SwapWeapon()
    {
        if(obtainedRangedWeapon)
            swappedWeapon = !swappedWeapon;

        if(swappedWeapon)
            inventoryState = 1;
        else
            inventoryState = 0;

        playerController.anim.SetInteger("InventoryState", inventoryState);
    }

    //calculates player health with the damage values from the EnemyInfos.cs script
    public void CalculatePlayerHealth(float dmg) 
    {
        if(isDamaged)
            playerHealth -= dmg;
        if(playerHealth <= 0)
            isAlive = false;

        healthBarBehaviour.FadingBarBehaviour();
    }

    //gives back a value from 0-1 for the player Health, Stamina and UltCharge
    void PlayerStatPercentage()
    {
        playerHealthPercentage = playerHealth / playerMaxHealth;
        playerStaminaPercentage = playerStamina / playerMaxStamina;
    }
    
    //Resets player values
    void ResetPlayer()
    {
        playerHealth = playerMaxHealth;
        transform.position = startingPos;
        respawnTimer = respawnTimerInit;
        PlayerStatPercentage();
        healthBarBehaviour.SetStat(playerHealthPercentage);
    }
}
