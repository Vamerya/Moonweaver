using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    PlayerController playerController;
    PlayerCombat playerCombat;
    Vector3 startingPos;
    public float playerMaxHealth, playerHealth, playerStamina, respawnTimer, respawnTimerInit, invincibilityTimer, invincibilityTimerInit;
    public int inventoryState;
    public bool obtainedRangedWeapon, swappedWeapon, isAlive, isDamaged;



    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();

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

    void CalculatePlayerHealth()
    {
        if(isDamaged)
            playerHealth -= 1;

        if(playerHealth == 0)
            isAlive = false;
    }
    
    void ResetPlayer()
    {
        playerHealth = playerMaxHealth;
        transform.position = startingPos;
        respawnTimer = respawnTimerInit;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isDamaged)
        {
            isDamaged = true;
            invincibilityTimer = invincibilityTimerInit;
            CalculatePlayerHealth();
        }
    }
}
