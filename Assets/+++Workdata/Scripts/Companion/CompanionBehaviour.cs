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
    public Collider2D boxCollider;
    Vector2 _distanceToTarget;
    float distanceToTarget;
    float stoppingRange = 1.5f;
    [SerializeField] float force;

    /// <summary>
    /// grabs reference to necessary components
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        rb.AddForce(new Vector2(force, force / 2), ForceMode2D.Impulse);
    }
    /// <summary>
    /// calculates how far the companion is away (on the X and Y axis) from the player based on the players position
    /// </summary>
    void Update()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.position);
        _distanceToTarget = target.position - transform.position;
        _distanceToTarget = _distanceToTarget.normalized;
        _distanceToTarget = _distanceToTarget * Random.Range(90, 110);
    }

    /// <summary>
    /// pushes the companion into the direction of the player based on its distance from them
    /// </summary>
    void FixedUpdate()
    {
        if(distanceToTarget > stoppingRange)
                rb.AddForce(_distanceToTarget);
    }
}
