using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBehaviour : MonoBehaviour
{
    PlayerInfos playerInfos;

    void Awake()
    {
        playerInfos = gameObject.GetComponentInParent<PlayerInfos>();
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Enemy") && !playerInfos.isDamaged)
        {
            playerInfos.isDamaged = true;
            playerInfos.invincibilityTimer = playerInfos.invincibilityTimerInit;
            playerInfos.CalculatePlayerHealth(collision.gameObject.GetComponent<EnemyInfos>().moonLightDamageHP.y);
        }
    }
}
