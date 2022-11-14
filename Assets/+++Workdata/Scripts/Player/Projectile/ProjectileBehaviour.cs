using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behaviour of the projectile the player shoots
/// </summary>
public class ProjectileBehaviour : MonoBehaviour
{
    #region Variables
    public Vector3 targetPos, moveDir;
    public float moveSpeed;

    public float despawnTimer;

    public float damage;
    #endregion

    /// <summary>
    /// controls the direction and speed of flight
    /// the despawn timer starts counting down as soon as the projectile gets instantiated
    /// when the timer reaches 0 this gameObject (projectile) gets destroyed
    /// </summary>
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;

        if(despawnTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// determines the direction of the projectile
    /// determines the damage of the projectile
    /// </summary>
    /// <param name="targetPos">based on the players mouse position on the screen</param>
    /// <param name="dmg">used to determine the damage of the projectile</param>
    public void ProjectileSetup(Vector3 targetPos, float dmg)
    {
        this.targetPos = targetPos;
        this.targetPos.z = 0;
        damage = dmg;
        moveDir = (this.targetPos - transform.position).normalized;
        moveDir.z = 0;
    }

    /// <summary>
    /// used to damage the enemy based on the damage
    /// </summary>
    /// <returns>the value that's used to damage the enemy</returns>
    public float DamageEnemyRanged()
    {
        return damage;
    }

    /// <summary>
    /// destroys the bullet when it collides with an object in the scene
    /// </summary>
    /// <param name="collision">CompareTag is used to determine what the projectile collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
