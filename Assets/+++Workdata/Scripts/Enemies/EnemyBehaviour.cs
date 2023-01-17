using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering.Universal;

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
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isMoving;



    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
        aiPath = gameObject.GetComponent<AIPath>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        enemyInfos.DetermineEnemyType(enemyID);
    }

    void Update()
    {
        FaceVelocity();
    }

    /// <summary>
    /// Checks what the desired velocity is and flips the sprite based on the direction
    /// </summary>
    void FaceVelocity()
    {
        direction = aiPath.desiredVelocity;


        if(direction.x < 0)
        {
            spriteRenderer.flipX = true;        //left
            GetComponentInChildren<BoxCollider2D>().transform.localScale = new Vector3(-1, 1, 1);
            GetComponentInChildren<Light2D>().transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(direction.x > 0)
        {
            spriteRenderer.flipX = false;       //right
            GetComponentInChildren<BoxCollider2D>().transform.localScale = new Vector3(1, 1, 1);
            GetComponentInChildren<Light2D>().transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
