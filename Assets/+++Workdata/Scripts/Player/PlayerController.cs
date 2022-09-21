using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim;
    SpriteRenderer playerSpriteRenderer;
    PlayerInfos playerInfos;
    PlayerCombat playerCombat;

    public float speed;
    public float movementX, movementY;
    private bool isMoving, isAttacking;
    public bool isInteracting;
    private InputActions inputActions;

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Awake()
    {
        inputActions = new InputActions();

        //KEYBOARD AND MOUSE
        inputActions.PlayerKeyboardMouseActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inputActions.PlayerKeyboardMouseActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inputActions.PlayerKeyboardMouseActionMap.Interact.performed += ctx => Interact(true);
        inputActions.PlayerKeyboardMouseActionMap.Interact.canceled += ctx => Interact(false);
        
        inputActions.PlayerKeyboardMouseActionMap.Attack.performed += ctx => playerCombat.Attack();
        inputActions.PlayerKeyboardMouseActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inputActions.PlayerKeyboardMouseActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();


        //CONTROLLER
        inputActions.PlayerControllerActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inputActions.PlayerControllerActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inputActions.PlayerControllerActionMap.Interact.performed += ctx => Interact(true);
        inputActions.PlayerControllerActionMap.Interact.canceled += ctx => Interact(false);
        
        inputActions.PlayerControllerActionMap.Attack.performed += ctx => playerCombat.Attack();
        inputActions.PlayerControllerActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inputActions.PlayerControllerActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();


    }

    void Start()
    {
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    void Update()
    {
        if(!playerInfos.isAlive)
            inputActions.Disable();
        else
            inputActions.Enable();

        anim.SetBool("isAlive", playerInfos.isAlive);
    }

    void FixedUpdate()
    {
        Vector2 move;
        move = new Vector2(movementX * speed, movementY * speed);
        rb.velocity = move;
    }

    //Method gives back Vector2 values for the playermovement
    void Movement(Vector2 direction)
    {
        movementX = direction.x;
        movementY = direction.y;

        if(movementX != 0 || movementY != 0)
            isMoving = true;

        else
            isMoving = false;

        anim.SetFloat("MovementX", movementX);
        anim.SetFloat("MovementY", movementY);
        anim.SetBool("isMoving", isMoving);
    }

    //checks whether the player is interacting via assigned button and sets bool accordingly
    void Interact(bool i)
    {
        isInteracting = i;
    }
}
