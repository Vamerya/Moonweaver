using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerController playerController;
    PlayerInfos playerInfos;
    public int attackState;
    public float attackTimer, attackTimerInit, chargingTimer, chargingTimerGoal;
    public bool isAttacking, isCharging, attackReleased;
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
    }
    public void Attack()
    {
        if(playerInfos.inventoryState == 0)
        {
            if(attackState == 0)
            {
                attackTimer = attackTimerInit;
                chargingTimer = 0;
                Debug.Log("Attack 1");   
            }
            else if(attackState == 1)
            {
                AttackFollowup1();
            }
            else if(attackState == 2)
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
                
        }
        else 
        {
            Debug.Log("Ranged weapon equipped");
            //Shooting stuff
        }
    }

    void AttackFollowup1()
    {
        attackTimer = attackTimerInit;
        chargingTimer = 0;
        Debug.Log("Attack 2");
    }
    
    void AttackFollowup2()
    {
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
        isCharging = false;
        chargingTimer = 0;
        StopAttacking();

        Debug.Log("Heavy attack");
    }

    public void StopAttacking()
    {
        isAttacking = false;
        playerController.anim.SetBool("isAttacking", isAttacking);
    }
}
