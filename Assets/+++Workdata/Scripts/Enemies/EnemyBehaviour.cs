using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Controls the general Behaviour of the enemies
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    public AIPath aiPath;
    Vector2 direction;
    public EnemyInfos enemyInfos;
    public int enemyID;

    public Animator anim;
    SpriteRenderer spriteRenderer;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isMoving;



    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
        aiPath = gameObject.GetComponent<AIPath>();
        anim = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        enemyInfos.DetermineEnemyType(enemyID);
    }

    void Update()
    {
        FaceVelocity();
    }

    void FaceVelocity()
    {
        direction = aiPath.desiredVelocity;


        if(direction.x < 0)
            spriteRenderer.flipX = true;        //left
        else
            spriteRenderer.flipX = false;       //right
    }
}
