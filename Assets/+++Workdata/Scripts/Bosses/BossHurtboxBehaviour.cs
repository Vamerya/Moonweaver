using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtboxBehaviour : MonoBehaviour
{
    [SerializeField] BossInfos bossInfos;

    void Awake()
    {
        bossInfos = GetComponentInParent<BossInfos>();
    }

    /// <summary>
    /// calls the  BossTakeDamage upon colliding with either the players melee weapon or one of the projectiles, grabbing references to either 
    /// weapons behaviour in order to input the damage
    /// if the players faith level is above 1 the enemy is also burning with the amount of damage input from the respective function
    /// </summary>
    /// <param name="collision">checks what the enemy was hit with and grabs references to specific scripts from that collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            bossInfos.knockbackDistance = collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineKnockbackDistance() * (1 - bossInfos.knockbackResistance);
            bossInfos.knockBackSpeed = collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineKnockbackSpeed();
            bossInfos.CalculateKnockbackPos(collision.transform);
            bossInfos.BossTakeDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DamageEnemyMelee());
            if (collision.GetComponentInParent<PlayerLevelBehaviour>().faith > 1)
            {
                bossInfos.isBurning = true;
                bossInfos.burningTimer = bossInfos.burningTimerInit;
                StartCoroutine(bossInfos.BossTakeBurnDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineBurningDamage()));
            }
            //Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }
}
