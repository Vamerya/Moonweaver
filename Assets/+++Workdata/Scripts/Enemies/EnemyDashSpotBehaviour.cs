using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashSpotBehaviour : MonoBehaviour
{
    [SerializeField] EnemyInfos enemyInfos;
    [SerializeField] PlayerController playerController;
    [SerializeField] Vector3 playerPosition;
    [SerializeField] Vector3 distanceToPlayer;
    [SerializeField] Vector3 dashSpotPos;

    void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        enemyInfos = gameObject.GetComponentInParent<EnemyInfos>();
    }

    void Start()
    {
        playerPosition = playerController.transform.position;
    }

    void Update()
    {
        distanceToPlayer = enemyInfos.transform.position - playerPosition;
        this.transform.position = distanceToPlayer * -1;
        dashSpotPos = this.transform.position;
    }
}
