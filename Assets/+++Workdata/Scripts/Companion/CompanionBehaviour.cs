using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;
    Vector2 distanceToPlayerX;
    float distanceToPlayer;
    float followRange = 1; 
    float stoppingRange = 1.5f;
    [SerializeField] float force;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        distanceToPlayerX = target.position - transform.position;
        distanceToPlayerX = distanceToPlayerX.normalized;
        distanceToPlayerX = distanceToPlayerX * force;
    }

    void FixedUpdate()
    {
        if(distanceToPlayer > stoppingRange)
                rb.AddForce(distanceToPlayerX);
    }
}
