using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used mainly for playerMovement as well and some menu toggles
/// </summary>
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

    /// <summary>
    /// assigns all the methods to their respective key-/button binds
    /// </summary>
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

    /// <summary>
    /// grabs references to components as well as the other scripts necessary for the player
    /// sets the playerSpeed to their maxSpeed value
    /// </summary>
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

    /// <summary>
    /// loads saved data
    /// loads default data if there's no saved data
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(GameData data)
    {
       this.transform.position = data.playerPos;

    }

    /// <summary>
    /// saves the data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        data.playerPos= this.transform.position;
    }

    /// <summary>
    /// disables the playerInput if the player is dead, enables it if the player is alive and sets according bool in animator
    /// 
    /// dashBufferTimer is the cooldown between dashes, if it's 0 and the player has enough stamina they can dash again
    /// 
    /// flips the sprite depending on the movement on the X axis
    /// 
    /// reduces playerSpeed while attacking or charging a heavy attack, resets it back to its maxSpeed if neither of those are happening
    /// 
    /// if the inventory state is 1 the playerSprites are now determined by the position of the mouse in relation to the player and sets according 
    /// parameters in the animator
    /// 
    /// the DetermineDirectionState gets called 
    /// </summary>
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

    /// <summary>
    /// player is moved depending on the input of the player
    /// 
    /// if the player can dash and is inputting the key, isDashing gets sets set to true, canDash to false, the trailrenderer is emitting during the dash
    /// and the dashingDirection is determined by the movementDirection of the player
    /// if the direction would be 0 the direction just gets set to 1, 1
    /// afterwards the StopDashing method gets called
    /// 
    /// if the player is dashing, the dashingDirection gets normalized and multiplied by the dashSpeed and applied to the rb.velocity,
    /// the dashBufferTimer is reset to the dashBufferLength (dash cooldown) 
    /// sets according parameter in player animator
    /// </summary>
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

    /// <summary>
    /// Method gives back Vector2 values for the playermovement, sets according values fo the playerAnimator
    /// </summary>
    /// <param name="direction">direction from the X and Y axis depending on the playerInput of the WASD keys</param>
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

    /// <summary>
    /// direction gets set to the mousePosition
    /// </summary>
    /// <param name="direction">used to determine where the mouse is in relation to the player</param>
    void Look(Vector3 direction)
    {
        mousePos = direction;
    }

    /// <summary>
    /// checks whether the player is interacting via assigned button and sets bool accordingly
    /// </summary>
    void Interact()
    {
        if(!isInteracting)
            isInteracting = true;
        else if(isInteracting)
            isInteracting = false;
    }

    /// <summary>
    /// toggles between the player hotbar and inventory, sets time scale to 0 when inventory is openend
    /// </summary>
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

    /// <summary>
    /// determines the direction the player last faced at before they stopped moving
    /// </summary>
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

    /// <summary>
    /// sets the dashInput depending on whether the player is inputting the key 
    /// </summary>
    /// <param name="value">bool that's given from the input of whether the player is pressing the assigned key</param>
    void Dash(bool value)
    {
        dashInput = value;
    }

    /// <summary>
    /// after being called the method is halted for the length of the dashingTime which causes the player to dash for that amount of time
    /// afterwards the trailRenderer stops emitting, isDashing gets set to false the players stamina is drained and applied to the staminaBar
    /// and the dashBufferTimer is reset to the dashBufferLength
    /// </summary>
    /// <returns>waits for the dashingTimer to reach 0 before continuing with the rest</returns>
    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
        playerInfos.playerStamina -= playerInfos.dashStaminaRequirement;
        staminaBarBehaviour.FadingBarBehaviour();
        dashBufferTimer = dashBufferLength;
    }
}
