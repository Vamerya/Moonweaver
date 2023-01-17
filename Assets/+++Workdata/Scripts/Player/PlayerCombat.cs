using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the attack states of the player as well heavy attacks, ultimate attacks and the according stamina drain 
/// for said attacks. Also manages the type of attack (melee/ranged) depending on the inventory state of the player
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    #region Variables
    [Header ("Main Components")]
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInfos playerInfos;
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

    [Header ("Bools Melee")]
    [SerializeField] public bool isAttacking;
    [SerializeField] public bool isCharging;
    [SerializeField] public bool chargeAttack;
    [SerializeField] bool staminaLight, staminaHeavy;
    [SerializeField] bool isUlting;
    [SerializeField] bool attackReleased;

    #endregion


    /// <summary>
    /// Grabs necessary references to other scripts
    /// </summary>
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }


    /// <summary>
    /// Mainly controls the timer used to increase attackState with successive attacks or reset the state back to 0 after the timer reached 0
    /// also controls the parameters for the playerAnimator as well as some necessary bools
    /// </summary>
    void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else 
        {
            attackState = 0;
            StopAttacking();
        }

        if(!attackReleased && isCharging)
        {
            chargingTimer += Time.deltaTime;

            if(chargingTimer > chargingTimerGoal)
                ChargeAttack();
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

    /// <summary>
    /// is called when attacking
    /// checks whether the player has enough stamina, released their attack and which attackState the player currently has
    /// before calling the method with the according attack from 1-3
    /// if the attack is not released a counter will start counting up from 0, calling the HeavyAttack method when reaching its goal
    /// 
    /// depending on the inventoryState the player either attacks (inventoryState 0) melee or (inventoryState 1) ranged
    /// </summary>
    public void Attack()
    {
        attackReleased = false;

        if (playerInfos.playerStamina > requiredStaminaLight && !isAttacking)
        {
            switch (attackState)
            {
                case 1:
                    Attack1();
                    break;
                case 2:
                    Attack2();
                    break;
                case 3:
                    Attack3();
                    break;
                default:
                    attackState = 1;
                    Attack1();
                    break;
            }
        }
        else
        {
            //StopAttacking();
            //attackState = 0;
        }

        ChargeAttackCharge();

        staminaBarBehaviour.FadingBarBehaviour();
    }

    /// <summary>
    /// if the player has enough stamina, the required stamina gets removed from the player, the attack timer is reset to the inital attackTimer
    /// isAttacking is set to true and the charging timer gets reset to 0 
    /// while attacking the player can't dash
    /// </summary>
    void Attack1()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        isAttacking = true;
        chargingTimer = 0;
        playerController.canDash = false;
        //Debug.Log("Attack 1");  
    }

    /// <summary>
    /// if the player has enough stamina, the required stamina gets removed from the player, the attack timer is reset to the inital attackTimer
    /// isAttacking is set to true and the charging timer gets reset to 0 
    /// while attacking the player can't dash
    /// </summary>
    void Attack2()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        isAttacking = true;
        chargingTimer = 0;
        playerController.canDash = false;
        //Debug.Log("Attack 2");
    }

    /// <summary>
    /// if the player has enough stamina, the required stamina gets removed from the player, the attack timer is reset to the inital attackTimer
    /// isAttacking is set to true and the charging timer gets reset to 0 
    /// while attacking the player can't dash
    /// </summary>
    void Attack3()
    {
        playerInfos.playerStamina -= requiredStaminaLight;
        attackTimer = attackTimerInit;
        isAttacking = true;
        chargingTimer = 0;
        playerController.canDash = false;
        //Debug.Log("Attack 3");
    }

    /// <summary>
    /// if the attack is released before the attackTimer reaches 0, the attackState gets increased by one, ensuring the next attack is gonna be the
    /// next part of the combo
    /// 
    /// attackReleased gets set to true and isCharging to false
    /// </summary>
    public void AttackRelease()
    {
        if (attackTimer > 0)
        {
            //attackState++;
        }

        attackReleased = true;
        isCharging = false;
    }

    /// <summary>
    /// while the player doesn't release the button to attack isCharging gets set to true, causing the heavyChargeTimer to count up
    /// otherwise if the attack is released the StopAttacking method gets called
    /// </summary>
    void ChargeAttackCharge()
    {
        if(!attackReleased)
            {
                isCharging = true;
                isAttacking = true;
            }
        else if (attackReleased)
            {
                StopAttacking();
            }
    }

    /// <summary>
    /// similar to the normal attacks the script first checks whether the player has enough stamina
    /// if so the required stamina gets reduced from the players stamina, isCharging gets set to false, the chargingTimer is reset to 0
    /// and the StopAttacking method is called
    /// </summary>
    public void ChargeAttack()
    {
        if(playerInfos.playerStamina > requiredStaminaHeavy)
        {
            chargeAttack = true;
            playerController.speed = .5f;
            playerInfos.playerStamina -= requiredStaminaHeavy;      
            isCharging = false;
            chargingTimer = 0;
            staminaBarBehaviour.FadingBarBehaviour();

            playerController.anim.SetBool("chargeAttack", chargeAttack);
        }
    }

    /// <summary>
    /// Sets the isAttacking bool to false, the player can dash again and the according parameter in the animator gets set
    /// This is called at the end of the attackAnimations as an event to ensure that everything is timed correctly
    /// </summary>
    public void StopAttacking()
    {
        isAttacking = false;
        chargeAttack = false;
        attackState++;
        playerController.canDash = true;
        playerController.speed = playerController.maxSpeed;
        playerController.anim.SetBool("isAttacking", isAttacking);
        playerController.anim.SetBool("chargeAttack", chargeAttack);
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
