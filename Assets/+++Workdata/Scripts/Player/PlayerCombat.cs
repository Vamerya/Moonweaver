using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region Variables
    [Header ("Main Components")]
    PlayerController playerController;
    PlayerInfos playerInfos;
    [SerializeField] StatBarBehaviour staminaBarBehaviour;

    [Header ("Timer")]
    [SerializeField] float attackTimer;
    [SerializeField] float attackTimerInit;
    [SerializeField] float chargingTimer;
    [SerializeField] float chargingTimerGoal;
    [SerializeField] float requiredStaminaLight;
    [SerializeField] float requiredStaminaHeavy;
    [SerializeField] float requiredStaminaUltimate;

    [Header ("Attack State")]
    [SerializeField] int attackState;

    [Header ("Bools")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isCharging;
    [SerializeField] bool isUlting;
    [SerializeField] bool attackReleased;
    #endregion

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            isAttacking = true;
            playerController.anim.SetBool("isAttacking", isAttacking);
        }
        else 
        {
            attackState = 0;
            playerController.anim.SetInteger("attackState", attackState);
        }

        if(!attackReleased && isAttacking)
            {
                isCharging = true;
                chargingTimer += Time.deltaTime;

                if(chargingTimer > chargingTimerGoal)
                {
                    HeavyAttack();
                }
            }
        else
            isCharging = false;

        playerController.anim.SetBool("isCharging", isCharging);
    }
    public void Attack()
    {
        if(playerInfos.inventoryState == 0)
        {
            if(attackState == 0 && playerInfos.playerStamina > requiredStaminaLight)
            {
                playerInfos.playerStamina -= requiredStaminaLight;
                attackTimer = attackTimerInit;
                chargingTimer = 0;
                Debug.Log("Attack 1");   
            }
            else if(attackState == 1 && playerInfos.playerStamina > requiredStaminaLight)
            {
                AttackFollowup1();
            }
            else if(attackState == 2 && playerInfos.playerStamina > requiredStaminaLight)
            {
                AttackFollowup2();
            }

            if(!attackReleased && attackTimer < 0)
            {
                isCharging = true;
                isAttacking = true;
                attackReleased = false;
            }
            else if (attackReleased)
            {
                isAttacking = false;
                attackReleased = true;   
            }
            attackReleased = false;
            
            staminaBarBehaviour.FadingBarBehaviour();
                
        }
        else 
        {
            Debug.Log("Ranged weapon equipped");
            //Shooting stuff
        }
    }

    void AttackFollowup1()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        chargingTimer = 0;
        Debug.Log("Attack 2");
    }
    
    void AttackFollowup2()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        attackState = -1;
        chargingTimer = 0;
        Debug.Log("Attack 3");
    }

    public void AttackRelease()
    {
        if(playerInfos.inventoryState == 0)
        {
            if(attackTimer > 0)
            {
                attackState++;
                playerController.anim.SetInteger("attackState", attackState);
            }

            attackReleased = true;
            isCharging = false;
            StopAttacking();
        }
    }

    public void HeavyAttack()
    {
        if(playerInfos.playerStamina > requiredStaminaHeavy)
        {
            playerInfos.playerStamina -= requiredStaminaHeavy;      
            isCharging = false;
            chargingTimer = 0;
            staminaBarBehaviour.FadingBarBehaviour();
            StopAttacking();

            Debug.Log("Heavy attack");
        }
    }

    public void StopAttacking()
    {
        isAttacking = false;
        playerController.anim.SetBool("isAttacking", isAttacking);
    }

    public void UltAttack()
    {
        if(playerInfos.playerStamina > requiredStaminaUltimate)
        {
            playerInfos.playerStamina -= requiredStaminaUltimate;
            isUlting = true;
            playerController.speed = 0;
        }
    }

    public void StopUlting()
    {
        isUlting = false;
        playerController.speed = playerController.maxSpeed;
    }
}
