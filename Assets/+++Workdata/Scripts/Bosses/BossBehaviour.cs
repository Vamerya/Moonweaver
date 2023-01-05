using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossBehaviour : MonoBehaviour
{
    public AIPath aiPath;
    Vector2 direction;
    public BossInfos bossInfos;
    public BossCombatBehaviour bossCombat;
    public int enemyID;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isMoving;



    void Awake()
    {
        bossCombat = gameObject.GetComponent<BossCombatBehaviour>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        bossInfos = gameObject.GetComponent<BossInfos>();
        aiPath = gameObject.GetComponent<AIPath>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        FaceVelocity();
        if (aiPath.desiredVelocity.x > 0 || aiPath.desiredVelocity.y > 0)
            isMoving = true;
        else
            isMoving = false;

        if(bossCombat.distanceToPlayer.x > 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        anim.SetBool("isMoving", isMoving);
    }

    void FaceVelocity()
    {
        direction = aiPath.desiredVelocity;


        if (direction.x < 0)
        {
            //spriteRenderer.flipX = true;        //left
            GetComponentInChildren<PolygonCollider2D>().transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(direction.x > 0)
        {
            //spriteRenderer.flipX = false;       //right
            GetComponentInChildren<PolygonCollider2D>().transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
