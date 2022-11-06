using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos : MonoBehaviour, IDataPersistence
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

    [Header ("Bools")]
    public bool isAlive;
    public bool obtainedRangedWeapon;
    public bool isDamaged;
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

    public void LoadData(GameData data)
    {
       this.playerLevel = data.playerLevel;
       this.obtainedRangedWeapon = data.obtainedRangedWeapon;
       this.transform.position = data.playerPos;
       if(data.obtainedRangedWeapon)
       {
            Destroy(GameObject.FindGameObjectWithTag("WeaponPickup"));
       }

    }

    public void SaveData(ref GameData data)
    {
        data.playerLevel = this.playerLevel;
        data.obtainedRangedWeapon = this.obtainedRangedWeapon;
        data.playerPos = this.transform.position;
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

        playerController.anim.SetFloat("InventoryState", inventoryState);
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
