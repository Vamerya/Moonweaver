using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    PlayerController playerController;
    PlayerCombat playerCombat;
    EnemyInfos enemyInfos;
    Vector3 startingPos;
    public float respawnTimer, respawnTimerInit, invincibilityTimer, invincibilityTimerInit;
    public float playerLevel, playerMaxHealth, playerHealth, playerMaxStamina, playerStamina;
    public int inventoryState;
    public bool obtainedRangedWeapon, swappedWeapon, isAlive, isDamaged;



    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();

        startingPos = transform.position;
        respawnTimer = respawnTimerInit;
        playerHealth = playerMaxHealth;
        invincibilityTimer = invincibilityTimerInit;
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

    //calculates player health 
    void CalculatePlayerHealth(float dmg) 
    {
        if(isDamaged)
            playerHealth -= dmg;

        if(playerHealth <= 0)
            isAlive = false;
    }
    
    //Resets player values
    void ResetPlayer()
    {
        playerHealth = playerMaxHealth;
        transform.position = startingPos;
        respawnTimer = respawnTimerInit;
    }

    //tracks collisions
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Enemy") && !isDamaged)
        {
            isDamaged = true;
            invincibilityTimer = invincibilityTimerInit;
            CalculatePlayerHealth(collision.gameObject.GetComponent<EnemyInfos>().runesDamageHP.y);
        }
    }
}
