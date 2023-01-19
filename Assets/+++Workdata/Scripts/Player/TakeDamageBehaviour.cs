using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls when the player takes damage and how much if they do
/// </summary>
public class TakeDamageBehaviour : MonoBehaviour
{
    PlayerInfos playerInfos;
    [SerializeField] PlayerStateManager playerState;

    /// <summary>
    /// grabs necessary reference to scripts
    /// </summary>
    void Awake()
    {
        playerInfos = gameObject.GetComponentInParent<PlayerInfos>();
    }

    /// <summary>
    /// gets called when the players hurtbox collides with the enemys hitbox, setting isDamaged to true and resetting the invincibility timer to ensure
    /// the player can't get hit again in that time frame
    /// afterwards the playerHealth gets recalculated and the damage of the enemy the player collided with gets put into that function
    /// </summary>
    /// <param name="collision">collision with an enemys hitbox</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitBox") && !playerInfos.isDamaged)
        {
            playerInfos.isDamaged = true;
            playerInfos.invincibilityTimer = playerInfos.invincibilityTimerInit;
            try
            {
                playerInfos.CalculatePlayerHealth(collision.gameObject.GetComponentInParent<EnemyInfos>().moonLightDamageHP.y);
            }
            catch
            {
                try
                {
                    playerInfos.CalculatePlayerHealth(collision.gameObject.GetComponentInParent<BossInfos>().bossDamage);
                }
                catch
                {
                    playerInfos.CalculatePlayerHealth(collision.gameObject.GetComponentInParent<SpikeBehaviour>().SpikeDamage());
                }
            }

            Debug.Log("KSKSKSKs");
        }
    }
}
