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
    [SerializeField] GameObject spikePrefab;
    [SerializeField] List<GameObject> summonedSpikes;

    [SerializeField] bool canAttack;
    [SerializeField] bool isAttacking;
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
            EnemyAttack(Random.Range(0, 3));
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
                BossIsAttacking();
                //StartCoroutine(StopAttacking());
                break;

            case 1:
                BossIsAttacking();
                //StartCoroutine(StopAttacking());
                break;

            case 2:
                //SummonSpike();
                //StartCoroutine(StopAttacking());
                break;

            default:
                Debug.Log("No Attack ID given");
                break;
        }

        Debug.LogWarning(_attackID);
        bossBehaviour.anim.SetInteger("attackState", _attackID);
        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

    /// <summary>
    /// controls the bool for the enemy and its animator
    /// </summary>
    void BossIsAttacking()
    {
        isAttacking = true;
        bossBehaviour.anim.SetBool("isAttacking", isAttacking);
    }

    void BossDodge()
    {

    }

    void SummonSpike()
    {
        isAttacking = true;
        //GameObject spike = Instantiate(spikePrefab, playerPosition.transform.position, Quaternion.identity);
        GameObject spike = Instantiate(spikePrefab, this.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10)), Quaternion.identity);
        spike.GetComponent<SpikeBehaviour>().DetermineSpikeDamage(bossInfos.bossDamage / 10);
        summonedSpikes.Add(spike);
    }
    
    void DestroySpike()
    {
        for (int i = 0; i < summonedSpikes.Count; i = 0)
        {
            Destroy(summonedSpikes[i]);
            summonedSpikes.RemoveAt(i);
        }
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
