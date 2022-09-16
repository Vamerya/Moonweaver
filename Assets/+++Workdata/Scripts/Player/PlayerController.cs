using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer playerSpriteRenderer;

    public float speed;
    public float movementX, movementY;
    private bool isMoving, isInteracting, isAttacking;
    private InputActions inputActions;

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.PlayerKeyboardMouseActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inputActions.PlayerKeyboardMouseActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inputActions.PlayerKeyboardMouseActionMap.Interact.performed += ctx => Interact(true);
        inputActions.PlayerKeyboardMouseActionMap.Interact.canceled += ctx => Interact(false);
        
        inputActions.PlayerKeyboardMouseActionMap.Attack.performed += ctx => Attack();
        inputActions.PlayerKeyboardMouseActionMap.Attack.canceled += ctx => AttackRelease();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 move;
        move = new Vector2(movementX * speed, movementY * speed);
        rb.velocity = move;

        Debug.Log(move);
    }

    private void Movement(Vector2 direction)
    {
        movementX = direction.x;
        movementY = direction.y;

        if(movementX != 0 || movementY != 0)
            isMoving = true;

        else
            isMoving = false;

        //anim.SetBool("isMoving", isMoving);
    }

    private void Interact(bool isInteracting)
    {
        isInteracting = !isInteracting;
    }

    private void Attack()
    {
        
    }
    
    private void AttackRelease()
    {
        
    }
}
