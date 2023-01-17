using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Controls the combat Behaviour of the enemies
/// </summary>
public class EnemyCombatBehaviour : MonoBehaviour
{
    public EnemyInfos enemyInfos;
    public EnemyBehaviour enemyBehaviour;
    public PlayerInRangeCheck playerInRangeCheck;
    PlayerController playerController;
    AIDestinationSetter destinationSetter;
    AIPath aiPath;
    Light2D eyeLight;
    Color defaultEyeColor;

    [SerializeField] Transform playerPosition;
    [SerializeField] GameObject dashSpot;

    [SerializeField] bool canAttack;
    [SerializeField] bool isAttacking;
    [SerializeField] int attackID;
    [SerializeField] float maxSpeed;
    [SerializeField] float attackTimer;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackCooldownInit;


    /// <summary>
    /// grabs all necessary references
    /// </summary>
    void Awake()
    {
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
        enemyBehaviour = gameObject.GetComponent<EnemyBehaviour>();
        playerInRangeCheck = gameObject.GetComponentInChildren<PlayerInRangeCheck>();
        eyeLight = gameObject.GetComponentInChildren<Light2D>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        playerPosition = playerController.transform;
        destinationSetter.target = playerPosition;
        aiPath = gameObject.GetComponent<AIPath>();
        maxSpeed = aiPath.maxSpeed;
    }

    void Start()
    {
        defaultEyeColor = eyeLight.color;
    }

    /// <summary>
    /// mainly for counters that determine whether the enemy can attack or not
    /// also generates a randomm ID for different attacks/moves every few seconds
    /// </summary>
    void Update()
    {
        if(attackCooldown > 0 )
            attackCooldown -= Time.deltaTime;

        if(playerInRangeCheck.playerInRange && attackCooldown <= 0)
        {
            canAttack = true;
        }
        if(playerInRangeCheck.playerInRange && attackCooldown < 1)
            eyeLight.color = Color.red;
        else
            canAttack = false;

        if(canAttack && !isAttacking)
            EnemyAttack(Random.Range(0, 3));
    }

    /// <summary>
    /// controls which attack/move the enemy is gonna do next based on the attack ID randomly generated every few seconds
    /// </summary>
    /// <param name="_attackID">takes in attackID to determine which attack/move should be performed</param>
    void EnemyAttack(int _attackID)
    {
        switch(_attackID)
        {
            case 0:
                EnemyAttack1(0);
                //StartCoroutine(StopAttacking());
                break;

            case 1:
                EnemyAttack1(1);
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

        enemyBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

    /// <summary>
    /// controls the bool for the enemy and its animator
    /// </summary>
    void EnemyAttack1(int _attackID)
    {
        isAttacking = true;
        enemyBehaviour.anim.SetBool("isAttacking", isAttacking);
        enemyBehaviour.anim.SetInteger("attackState", _attackID);
    }

    /// <summary>
    /// one of the dashing points is picked as a new follow target and then gets reset after a set amount of time
    /// </summary>
    void EnemyDodge()
    {
        isAttacking = true;
        destinationSetter.target = dashSpot.transform;
        aiPath.maxSpeed *= 1.1f;

        StartCoroutine(ResetFollowTarget());
    }

    IEnumerator ResetFollowTarget()
    {
        yield return new WaitForSecondsRealtime(1f);
        destinationSetter.target = playerPosition;
        yield return new WaitForSecondsRealtime(1.5f);
        aiPath.maxSpeed = maxSpeed;
        isAttacking = false;
    }

    /// <summary>
    /// controls the bool for the enemy and its animator and also resets its cooldown until the next attack
    /// </summary>
    void StopAttacking()
    {
        //yield return new WaitForSecondsRealtime(1f);
        isAttacking = false;
        eyeLight.color = defaultEyeColor;
        attackCooldown = attackCooldownInit;

        enemyBehaviour.anim.SetBool("isAttacking", isAttacking);
    }
}
