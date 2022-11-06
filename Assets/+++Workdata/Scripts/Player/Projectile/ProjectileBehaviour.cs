using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    #region Variables
    public Vector3 targetPos, moveDir;
    public float moveSpeed;

    public float despawnTimer;

    public float damage;
    #endregion

    void Start()
    {

    }
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;

        if(despawnTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ProjectileSetup(Vector3 targetPos, float dmg)
    {
        this.targetPos = targetPos;
        this.targetPos.z = 0;
        damage = dmg;
        moveDir = (this.targetPos - transform.position).normalized;
        moveDir.z = 0;
    }

    public float DamageEnemyRanged()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
