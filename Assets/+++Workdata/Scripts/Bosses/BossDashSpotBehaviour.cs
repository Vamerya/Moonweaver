using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashSpotBehaviour : MonoBehaviour
{
    [SerializeField] BossInfos bossInfos;
    [SerializeField] PlayerController playerController;
    [SerializeField] Vector3 playerPosition;
    [SerializeField] Vector3 distanceToPlayer;

    void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        bossInfos = gameObject.GetComponentInParent<BossInfos>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        playerPosition = playerController.transform.position;
        distanceToPlayer = playerPosition -     bossInfos.transform.position;
        this.transform.position = bossInfos.transform.position + (distanceToPlayer * -1);
    }
}
