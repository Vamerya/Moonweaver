using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header ("Main Components")]
    Rigidbody2D rb;
    public Animator anim;
    SpriteRenderer playerSpriteRenderer;
    PlayerInfos playerInfos;
    PlayerCombat playerCombat;

    [Header ("Inventory")]
    [SerializeField] public GameObject _playerInventory;
    [SerializeField] public GameObject _playerHotbar;

    [Header ("Movement, interaction and inventory")]
    public float maxSpeed;
    public float speed;
    public float movementX, movementY;
    bool isMoving, isAttacking;
    public bool isInteracting;
    public int inventoryHotbarState;

    [Header ("Dash Variables")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength;
    [SerializeField] private float dashBufferLength;
    float dashBufferCounter; 
    bool isDashing, hasDashed;
    bool canDash => dashBufferCounter > 0f && !hasDashed;

    [Header ("Input actions")]
    InputActions inGameInputActions;
    #endregion

    void OnEnable()
    {
        inGameInputActions.Enable();
        //menuInputActions.Enable();
    }

    void OnDisable()
    {
        inGameInputActions.Disable();
        //menuInputActions.Disable();
    }

    void Awake()
    {
        inGameInputActions = new InputActions();

        //KEYBOARD AND MOUSE
        inGameInputActions.PlayerKeyboardMouseActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inGameInputActions.PlayerKeyboardMouseActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inGameInputActions.PlayerKeyboardMouseActionMap.Interact.performed += ctx => Interact(true);
        inGameInputActions.PlayerKeyboardMouseActionMap.Interact.canceled += ctx => Interact(false);
        
        inGameInputActions.PlayerKeyboardMouseActionMap.Attack.performed += ctx => playerCombat.Attack();
        inGameInputActions.PlayerKeyboardMouseActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inGameInputActions.PlayerKeyboardMouseActionMap.Dash.performed += ctx => Dash();

        inGameInputActions.PlayerKeyboardMouseActionMap.OpenInventory.performed += ctx => InventoryToggle();

        inGameInputActions.PlayerKeyboardMouseActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();


        //CONTROLLER
        inGameInputActions.PlayerControllerActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inGameInputActions.PlayerControllerActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inGameInputActions.PlayerControllerActionMap.Interact.performed += ctx => Interact(true);
        inGameInputActions.PlayerControllerActionMap.Interact.canceled += ctx => Interact(false);
        
        inGameInputActions.PlayerControllerActionMap.Attack.performed += ctx => playerCombat.Attack();
        inGameInputActions.PlayerControllerActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inGameInputActions.PlayerControllerActionMap.Dash.performed += ctx => Dash();

        inGameInputActions.PlayerControllerActionMap.OpenInventory.performed += ctx => InventoryToggle();

        inGameInputActions.PlayerControllerActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();
    }

    void Start()
    {
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();

        speed = maxSpeed;
    }

    void Update()
    {
        if(!playerInfos.isAlive)
            inGameInputActions.Disable();
        else
            inGameInputActions.Enable();

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

    //toggles between the player hotbar and inventory, sets time scale to 0 when inventory is openend
    void InventoryToggle()
    {
        if(inventoryHotbarState == 0)
        {
            inventoryHotbarState = 1;
            _playerHotbar.SetActive(false);
            _playerInventory.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(inventoryHotbarState == 1)
        {
            inventoryHotbarState = 0;
            _playerHotbar.SetActive(true);
            _playerInventory.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void Dash()
    {

    }
}
