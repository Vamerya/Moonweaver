using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedWeaponBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] Transform projectileSpawner;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;

    public GameObject projectilePrefab;

    float bulletDamage;

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

    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawner.position, Quaternion.identity);

        newProjectile.GetComponent<ProjectileBehaviour>().ProjectileSetup(playerController.lookDir, DetermineBulletDamage());
    }

    float DetermineBulletDamage()
    {
        bulletDamage = Mathf.Log(playerLevelBehaviour.dexterity, 2) * 10;
        bulletDamage = Mathf.Floor(bulletDamage);
        return bulletDamage;
    }
}
