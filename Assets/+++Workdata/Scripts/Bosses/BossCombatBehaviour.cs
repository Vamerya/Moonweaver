using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossCombatBehaviour : MonoBehaviour
{
    public BossInfos bossInfos;
    public BossBehaviour bossBehaviour;
    public PlayerInRangeCheck playerInRangeCheck;
    PlayerController playerController;
    AIDestinationSetter destinationSetter;

    [SerializeField] Transform playerPosition;

    [SerializeField] bool canAttack;
    [SerializeField] bool isAttacking;
    [SerializeField] int attackID;
    [SerializeField] float attackTimer;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackCooldownInit;


    /// <summary>
    /// grabs all necessary references
    /// </summary>
    void Awake()
    {
        bossInfos = gameObject.GetComponent<BossInfos>();
        bossBehaviour = gameObject.GetComponent<BossBehaviour>();
        playerInRangeCheck = gameObject.GetComponentInChildren<PlayerInRangeCheck>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        playerPosition = playerController.transform;
        destinationSetter.target = playerPosition;
    }

    void Start()
    {

    }

    /// <summary>
    /// mainly for counters that determine whether the enemy can attack or not
    /// also generates a randomm ID for different attacks/moves every few seconds
    /// </summary>
    void Update()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if (playerInRangeCheck.playerInRange && attackCooldown <= 0)
            canAttack = true;
        else
            canAttack = false;

        if (canAttack)
            EnemyAttack(Random.Range(0, 2));
    }

    /// <summary>
    /// controls which attack/move the enemy is gonna do next based on the attack ID randomly generated every few seconds
    /// </summary>
    /// <param name="_attackID">takes in attackID to determine which attack/move should be performed</param>
    void EnemyAttack(int _attackID)
    {
        switch (_attackID)
        {
            case 0:
                EnemyIsAttacking();
                //StartCoroutine(StopAttacking());
                break;

            case 1:
                EnemyIsAttacking();
                //StartCoroutine(StopAttacking());
                break;

            case 2:
                EnemyDodge();
                //StartCoroutine(StopAttacking());
                break;

            default:
                Debug.Log("No Attack ID given");
                break;
        }

        bossBehaviour.anim.SetInteger("attackState", _attackID);
        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

    /// <summary>
    /// controls the bool for the enemy and its animator
    /// </summary>
    void EnemyIsAttacking()
    {
        isAttacking = true;
        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

    void EnemyDodge()
    {

    }

    /// <summary>
    /// controls the bool for the enemy and its animator and also resets its cooldown until the next attack
    /// </summary>
    void StopAttacking()
    {
        //yield return new WaitForSecondsRealtime(1f);
        isAttacking = false;
        attackCooldown = attackCooldownInit;

        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }
}
