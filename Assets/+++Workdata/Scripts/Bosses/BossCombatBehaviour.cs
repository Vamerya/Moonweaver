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
    Rigidbody2D rb;
    AIPath aIPath;

    [SerializeField] Transform playerPosition;
    public Vector2 distanceToPlayer;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject[] dashSpots;
    [SerializeField] List<GameObject> summonedSpikes;

    [SerializeField] bool canAttack;
    [SerializeField] bool isAttacking;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackCooldownInit;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashCooldownInit;
    [SerializeField] float maxSpeed;

    [SerializeField] bool methodDone;


    /// <summary>
    /// grabs all necessary references
    /// </summary>
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bossInfos = gameObject.GetComponent<BossInfos>();
        bossBehaviour = gameObject.GetComponent<BossBehaviour>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        playerInRangeCheck = gameObject.GetComponentInChildren<PlayerInRangeCheck>();
        playerPosition = playerController.transform;
        destinationSetter.target = playerPosition;
        aIPath = gameObject.GetComponent<AIPath>();
        maxSpeed = aIPath.maxSpeed;
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

        if(dashCooldown > 0)
            dashCooldown -= Time.deltaTime;
        else
            BossDodge();

        if (canAttack && !isAttacking)
            EnemyAttack(Random.Range(0, 2));

        distanceToPlayer = transform.position - playerPosition.transform.position;
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
                Attack1();
                break;
                
            case 1:
                Attack2();
                break;

            default:
                Debug.Log("No Attack ID given");
                break;
        }

        Debug.LogWarning(_attackID);
        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

    /// <summary>
    /// controls the bool for the enemy and its animator
    /// </summary>
    void BossAttack(int _attackID)
    {
        isAttacking = true;
        bossBehaviour.anim.SetFloat("attackState", _attackID);
        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

/// <summary>
/// one of the dashing points is picked as a new follow target and then gets reset after a set amount of time
/// </summary>
    void BossDodge()
    {
        var dashSpot = Random.Range(0, dashSpots.Length);
        destinationSetter.target = dashSpots[dashSpot].transform;
        aIPath.maxSpeed *= 2;
        dashCooldown = Random.Range(dashCooldownInit - 2, dashCooldownInit + 2);
    }

    IEnumerator ResetFollowTarget()
    {
        yield return new WaitForSecondsRealtime(1f);
        destinationSetter.target = playerPosition;
        yield return new WaitForSecondsRealtime(1f);
        aIPath.maxSpeed = maxSpeed;
    }

    IEnumerator SummonSpike()
    {
        yield return new WaitForSecondsRealtime(1f);
        isAttacking = true;
        GameObject spike = Instantiate(spikePrefab, playerPosition.position, Quaternion.identity);
        spike.GetComponent<SpikeBehaviour>().DetermineSpikeDamage(bossInfos.bossDamage / 5);
        summonedSpikes.Add(spike);
    }
    
    void DestroySpike()
    {
        for (int i = 0; i < summonedSpikes.Count; i = 0)
        {
            Destroy(summonedSpikes[i]);
            summonedSpikes.RemoveAt(i);
            isAttacking = false;
        }
    }

    void Attack1()
    {
        isAttacking = true;
        BossDodge();
        StartCoroutine(SummonSpike());
        StartCoroutine(ResetFollowTarget());
        BossAttack(1);
    }

    void Attack2()
    {
        isAttacking = true;
        BossDodge();
        StartCoroutine(ResetFollowTarget());
        BossDodge();
        StartCoroutine(ResetFollowTarget());
        BossAttack(2);
    }

    /// <summary>
    /// controls the bool for the enemy and its animator and also resets its cooldown until the next attack
    /// </summary>
    void StopAttacking()
    {
        isAttacking = false;
        attackCooldown = attackCooldownInit;

        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }
}
