using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls how the players companion should move
/// </summary>
public class CompanionBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;
    Vector2 _distanceToPlayer;
    float distanceToPlayer;
    float followRange = 1; 
    float stoppingRange = 1.5f;
    [SerializeField] float force;

    /// <summary>
    /// grabs reference to necessary components
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// calculates how far the companion is away (on the X and Y axis) from the player based on the players position
    /// </summary>
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        _distanceToPlayer = target.position - transform.position;
        _distanceToPlayer = _distanceToPlayer.normalized;
        _distanceToPlayer = _distanceToPlayer * force;
    }

    /// <summary>
    /// pushes the companion into the direction of the player based on its distance from them
    /// </summary>
    void FixedUpdate()
    {
        if(distanceToPlayer > stoppingRange)
                rb.AddForce(_distanceToPlayer);
    }
}
