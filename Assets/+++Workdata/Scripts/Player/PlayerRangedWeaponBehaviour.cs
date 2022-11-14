using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the players ranged weapon
/// </summary>
public class PlayerRangedWeaponBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] Transform projectileSpawner;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;

    public GameObject projectilePrefab;

    float bulletDamage;

    // - Vigor -> Overall HP
    // - Endurance -> Overall stamina
    // - Mind -> Execute @ specific hp% && crit chance
    // - Strength -> do stuff but heavy
    // - Dexterity -> do stuff but fast || increase ranged weapon damage
    // - Faith -> burning damage
    // - Luck -> crit damage

    void Awake()
    {
        playerLevelBehaviour = gameObject.GetComponent<PlayerLevelBehaviour>();
        playerController = gameObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        //45, 135
        float eulerAnglesZ = transform.rotation.eulerAngles.z;

        // if(eulerAnglesZ < 135 && eulerAnglesZ > 45)
        //     weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;

        // else
        //     weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder;
    }

    /// <summary>
    /// instanties a new projectile based on the prefab when this method gets called
    /// the projectile gets spawned in the position/rotation of the projectileSpawner
    /// the direction of flight is dependent on the direction the player looks at 
    /// the damageAmount gets put in by the function DetermineBulletDamage
    /// </summary>
    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawner.position, Quaternion.identity);

        newProjectile.GetComponent<ProjectileBehaviour>().ProjectileSetup(playerController.lookDir, DetermineBulletDamage());
    }

    /// <summary>
    /// determines the damage of the projectiles based on the players dexterity level put into a logarithmic function with funky numbers
    /// </summary>
    /// <returns>the amount a projectile should deal</returns>
    float DetermineBulletDamage()
    {
        bulletDamage = Mathf.Log(playerLevelBehaviour.dexterity, 2) * 10;
        bulletDamage = Mathf.Floor(bulletDamage);
        return bulletDamage;
    }
}
