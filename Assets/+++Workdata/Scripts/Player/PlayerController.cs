using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header ("Main Components")]
    Rigidbody2D rb;
    public Animator anim;
    TrailRenderer trailRenderer;
    SpriteRenderer playerSpriteRenderer;
    PlayerInfos playerInfos;
    PlayerCombat playerCombat;
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayerHealthflaskBehaviour playerHealthflaskBehaviour;
    [SerializeField] StatBarBehaviour staminaBarBehaviour;
    [SerializeField] ShrineBehaviour shrineBehaviour;
    [SerializeField] LevelUpManager levelUpManager;
    [SerializeField] MenuButtons menuButtons;

    [Header ("Inventory")]
    [SerializeField] public GameObject _playerInventory;
    [SerializeField] public GameObject _playerHotbar;

    [Header ("Movement, interaction and inventory")]
    public float maxSpeed;
    public float speed;
    public float movementX, movementY, directionState;
    public Vector2 mousePos;
    public Vector3 lookDir;
    bool isMoving;
    public bool isInteracting, isTalking;
    public int inventoryHotbarState;

    [Header ("Dash Variables")]
    [SerializeField] float dashBufferLength;
    [SerializeField] float dashingVelocity;
    [SerializeField] float dashingTime;
    [SerializeField] public bool canDash;
    Vector2 dashingDir;
    float dashBufferTimer;
    bool dashInput;
    bool isDashing;

    [Header ("Input actions")]
    public InputActions inGameInputActions;
    #endregion

    #region Input Enable/Disable
    void OnEnable()
    {
        inGameInputActions.Enable();
    }

    void OnDisable()
    {
        inGameInputActions.Disable();
    }
    #endregion

    void Awake()
    {
        inGameInputActions = new InputActions();

        //KEYBOARD AND MOUSE
        inGameInputActions.PlayerKeyboardMouseActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inGameInputActions.PlayerKeyboardMouseActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inGameInputActions.PlayerKeyboardMouseActionMap.Interact.performed += ctx => Interact();
        inGameInputActions.PlayerKeyboardMouseActionMap.UseFlask.performed += ctx => playerHealthflaskBehaviour.UseFlask();
        
        inGameInputActions.PlayerKeyboardMouseActionMap.Attack.performed += ctx => playerCombat.Attack();
        inGameInputActions.PlayerKeyboardMouseActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inGameInputActions.PlayerKeyboardMouseActionMap.Dash.performed += ctx => Dash(true);
        inGameInputActions.PlayerKeyboardMouseActionMap.Dash.canceled += ctx => Dash(false);

        inGameInputActions.PlayerKeyboardMouseActionMap.OpenInventory.performed += ctx => InventoryToggle();

        inGameInputActions.PlayerKeyboardMouseActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();

        inGameInputActions.PlayerKeyboardMouseActionMap.TogglePauseMenu.performed += ctx => menuButtons.TogglePauseMenu();

        inGameInputActions.PlayerKeyboardMouseActionMap.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());


        //CONTROLLER
        inGameInputActions.PlayerControllerActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inGameInputActions.PlayerControllerActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inGameInputActions.PlayerControllerActionMap.Interact.performed += ctx => Interact();
        inGameInputActions.PlayerControllerActionMap.UseFlask.performed += ctx => playerHealthflaskBehaviour.UseFlask();

        
        inGameInputActions.PlayerControllerActionMap.Attack.performed += ctx => playerCombat.Attack();
        inGameInputActions.PlayerControllerActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inGameInputActions.PlayerControllerActionMap.Dash.performed += ctx => Dash(true);
        inGameInputActions.PlayerControllerActionMap.Dash.canceled += ctx => Dash(false);

        inGameInputActions.PlayerControllerActionMap.OpenInventory.performed += ctx => InventoryToggle();

        inGameInputActions.PlayerControllerActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();

        inGameInputActions.PlayerControllerActionMap.TogglePauseMenu.performed += ctx => menuButtons.TogglePauseMenu();
    }

    void Start()
    {
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
        playerHealthflaskBehaviour = gameObject.GetComponent<PlayerHealthflaskBehaviour>();

        speed = maxSpeed;
    }
    public void LoadData(GameData data)
    {
       this.transform.position = data.playerPos;

    }

    public void SaveData(ref GameData data)
    {
        data.playerPos= this.transform.position;
    }
    void Update()
    {
        if(!playerInfos.isAlive || isTalking)
            inGameInputActions.Disable();
        else
            inGameInputActions.Enable();

        anim.SetBool("isAlive", playerInfos.isAlive);

        if(dashBufferTimer > 0)
        {
            dashBufferTimer -= Time.deltaTime;
            canDash = false;
        }
        else if(playerInfos.playerStamina > playerInfos.dashStaminaRequirement)
            canDash = true;

        if(movementX < 0)
            playerSpriteRenderer.flipX = true;
        else if(movementX > 0)
            playerSpriteRenderer.flipX = false;

        if(playerCombat.isAttacking || playerCombat.isCharging)
            speed = maxSpeed / 3;
        else
            speed = maxSpeed;


        if(playerInfos.inventoryState == 1)
        {
            lookDir = mainCamera.ScreenToWorldPoint(mousePos);
            var dir = lookDir.normalized;
            // playerGun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));

            anim.SetFloat("LookDirX", dir.x);
            anim.SetFloat("LookDirY", dir.y);

            if (dir.x > 0) //right
                playerSpriteRenderer.flipX = false;

            else if (dir.x < 0) //left
                playerSpriteRenderer.flipX = true;
        }

        DetermineDirectionState();
    }

    void FixedUpdate()
    {
        Vector2 move;
        move = new Vector2(movementX * speed, movementY * speed);
        rb.velocity = move;

        if(dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = move;
            if(dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            StartCoroutine(StopDashing());
        }

        if(isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            dashBufferTimer = dashBufferLength;
            return;
        }


        anim.SetBool("isDashing", isDashing);
    }

    //Method gives back Vector2 values for the playermovement, sets according values fo the playerAnimator
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

    void Look(Vector3 direction)
    {
        mousePos = direction;
    }

    //checks whether the player is interacting via assigned button and sets bool accordingly
    void Interact()
    {
        if(!isInteracting)
            isInteracting = true;
        else if(isInteracting)
            isInteracting = false;
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

    void DetermineDirectionState()
    {
        if (movementX == 0 && movementY > 0)        //Up
            directionState = 0;
        else if(movementX > 0 && movementY > 0)     //UpRight
            directionState = 1;
        else if(movementX > 0 && movementY == 0)    //Right
            directionState = 2;
        else if(movementX > 0 && movementY < 0)     //DownRight
            directionState = 3;
        else if(movementX == 0 && movementY < 0)    //Down
            directionState = 4;
        else if(movementX < 0 && movementY < 0)     //DownLeft
            directionState = 5;
        else if(movementX < 0 && movementY == 0)    //Left
            directionState = 6;
        else if(movementX < 0 && movementY > 0)     //UpLeft
            directionState = 7;

        anim.SetFloat("DirectionState", directionState);
    }

    void Dash(bool i)
    {
        dashInput = i;
    }

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
        staminaBarBehaviour.FadingBarBehaviour();
        playerInfos.playerStamina -= playerInfos.dashStaminaRequirement;
        dashBufferTimer = dashBufferLength;
    }
}
