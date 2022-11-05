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
    [SerializeField] public float chargingTimerGoal;
    [SerializeField] float requiredStaminaLight;
    [SerializeField] float requiredStaminaHeavy;
    [SerializeField] float requiredStaminaUltimate;

    [Header ("Attack State")]
    [SerializeField] int attackState;

    [Header ("Bools")]
    [SerializeField] public bool isAttacking;
    [SerializeField] public bool isCharging;
    [SerializeField] bool staminaLight, staminaHeavy;
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
        }
        else 
        {
            attackState = 0;
        }

        if(!attackReleased && isCharging)
        {
            chargingTimer += Time.deltaTime;

            if(chargingTimer > chargingTimerGoal)
                HeavyAttack();
        }
        else    
            isCharging = false;

        if(playerInfos.playerStamina > requiredStaminaLight)
            staminaLight = true;
        else
            staminaLight = false;

        if(playerInfos.playerStamina > requiredStaminaHeavy)
            staminaHeavy = true;
        else
            staminaHeavy = false;   

        playerController.anim.SetFloat("attackState", attackState);
        playerController.anim.SetBool("isAttacking", isAttacking);
        playerController.anim.SetBool("isCharging", isCharging);
        playerController.anim.SetBool("staminaLight", staminaLight);
        playerController.anim.SetBool("staminaHeavy", staminaHeavy);
    }
    public void Attack()
    {
        if(playerInfos.inventoryState == 0)
        {
            attackReleased = false;

            if(attackState == 0 && playerInfos.playerStamina > requiredStaminaLight && !isAttacking)
            {
                Attack1();
            }
            else if(attackState == 1 && playerInfos.playerStamina > requiredStaminaLight && !isAttacking)
            {
                Attack2();
            }
            else if(attackState == 2 && playerInfos.playerStamina > requiredStaminaLight && !isAttacking)
            {
                Attack3();
            }
            else if(attackState > 2 && playerInfos.playerStamina > requiredStaminaLight && !isAttacking)
            {
                attackState = 0;
                Attack1();
            }

            HeavyAttackCharge();
            
            staminaBarBehaviour.FadingBarBehaviour();
                
        }
        else 
        {
            Debug.Log("Ranged weapon equipped");
            //shooting but it comes back!
        }
    }

    void Attack1()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        isAttacking = true;
        chargingTimer = 0;
        playerController.canDash = false;
        //Debug.Log("Attack 1");  
    }

    void Attack2()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        isAttacking = true;
        chargingTimer = 0;
        playerController.canDash = false;
        //Debug.Log("Attack 2");
    }
    
    void Attack3()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        isAttacking = true;
        chargingTimer = 0;
        playerController.canDash = false;
        //Debug.Log("Attack 3");
    }

    public void AttackRelease()
    {
        if(playerInfos.inventoryState == 0)
        {
            if(attackTimer > 0)
            {
                attackState++;
            }

            attackReleased = true;
            isCharging = false;
        }
    }

    void HeavyAttackCharge()
    {
        if(!attackReleased && attackTimer > 0)
            {
                isCharging = true;
                isAttacking = true;
            }
            else if (attackReleased)
            {
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
        playerController.canDash = true;
        playerController.anim.SetBool("isAttacking", isAttacking);
    }

    public void UltAttack()
    {
        if(playerInfos.playerStamina > requiredStaminaUltimate)
        {
            playerInfos.playerStamina -= requiredStaminaUltimate;
            isUlting = true;
            playerController.speed = .5f;
        }
    }

    public void StopUlting()
    {
        isUlting = false;
        playerController.speed = playerController.maxSpeed;
    }
}
