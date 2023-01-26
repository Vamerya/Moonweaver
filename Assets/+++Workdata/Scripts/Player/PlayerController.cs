using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mainly used for playerMovement as well and some menu toggles
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Main Components")]
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] PlayerSittingBehaviour sittingBehaviour;
    [SerializeField] PlayerHealthflaskBehaviour playerHealthflaskBehaviour;
    [SerializeField] PlayerSoundBehaviour playerSoundBehaviour;
    [SerializeField] StatBarBehaviour staminaBarBehaviour;
    [SerializeField] LevelUpManager levelUpManager;
    [SerializeField] ShrineManager shrineManager;
    [SerializeField] MenuButtons menuButtons;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public Animator anim;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] Transform playerRangedWeapon;

    [Header("Inventory")]
    [SerializeField] GameObject _storeMoonFragmentsButton;

    [Header("Movement, interaction and inventory")]
    public float maxSpeed; 
    public float speed;
    public float movementX, movementY, directionState;
    public Vector2 lookDir;
    public bool isMoving, isSitting;
    public bool isInteracting, isTalking, shrineNearby;
    public bool isMainShrine;
    public int inventoryHotbarState;

    [Header("Dash Variables")]
    [SerializeField] float dashBufferLength;
    [SerializeField] float dashingVelocity;
    [SerializeField] float dashingTime;
    [SerializeField] public bool canDash;
    public Vector2 dashingDir;
    float dashBufferTimer;
    bool dashInput;
    [SerializeField] bool isDashing;

    [Header("Input actions")]
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

        inGameInputActions.PlayerKeyboardMouseActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();

        inGameInputActions.PlayerKeyboardMouseActionMap.TogglePauseMenu.performed += ctx => menuButtons.TogglePauseMenu();


        //XBOX CONTROLLER
        inGameInputActions.PlayerXBOXActionMap.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        inGameInputActions.PlayerXBOXActionMap.Movement.canceled += ctx => Movement(ctx.ReadValue<Vector2>());

        inGameInputActions.PlayerXBOXActionMap.Interact.performed += ctx => Interact();
        inGameInputActions.PlayerXBOXActionMap.UseFlask.performed += ctx => playerHealthflaskBehaviour.UseFlask();

        inGameInputActions.PlayerXBOXActionMap.Attack.performed += ctx => playerCombat.Attack();
        inGameInputActions.PlayerXBOXActionMap.Attack.canceled += ctx => playerCombat.AttackRelease();

        inGameInputActions.PlayerXBOXActionMap.Dash.performed += ctx => Dash(true);
        inGameInputActions.PlayerXBOXActionMap.Dash.canceled += ctx => Dash(false);

        inGameInputActions.PlayerXBOXActionMap.SwapWeapon.performed += ctx => playerInfos.SwapWeapon();

        inGameInputActions.PlayerXBOXActionMap.TogglePauseMenu.performed += ctx => menuButtons.TogglePauseMenu();
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
        sittingBehaviour = gameObject.GetComponent<PlayerSittingBehaviour>();

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
        data.playerPos = this.transform.position;
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
        if (!playerInfos.isAlive || isTalking)
            inGameInputActions.Disable();
        else
            inGameInputActions.Enable();

        anim.SetBool("isAlive", playerInfos.isAlive);

        if (dashBufferTimer > 0)
        {
            dashBufferTimer -= Time.deltaTime;
            canDash = false;
        }
        else if (playerInfos.playerStamina > playerInfos.dashStaminaRequirement)
            canDash = true;

        if (movementX < 0)
            playerSpriteRenderer.flipX = true;
        else if (movementX > 0)
            playerSpriteRenderer.flipX = false;

        DetermineDirectionState();

        anim.SetBool("isDashing", isDashing);
    }

    /// <summary>
    /// player is moved depending on the input of the player
    /// 
    /// if the player can dash and is inputting the key, isDashing gets set to true, canDash to false, the trailrenderer is emitting during the dash
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

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = move;
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(0, 0);
            }
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            dashBufferTimer = dashBufferLength;
            return;
        }
    }

    /// <summary>
    /// Method gives back Vector2 values for the playermovement, sets according values fo the playerAnimator
    /// </summary>
    /// <param name="direction">direction of the X and Y axis depending on the playerInput of the WASD keys</param>
    void Movement(Vector2 direction)
    {
        movementX = direction.x;
        movementY = direction.y;
        lookDir = direction;

        if (movementX != 0 || movementY != 0)
            isMoving = true;
        else
            isMoving = false;

        anim.SetFloat("MovementX", movementX);
        anim.SetFloat("MovementY", movementY);
        anim.SetBool("isMoving", isMoving);
    }

    /// <summary>
    /// checks whether the player is interacting via assigned button and sets bool accordingly
    /// also opens up the level up UI if a shrine is nearby
    /// </summary>
    void Interact()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            if (shrineNearby)
            {
                shrineManager.ShowShrineMenu();
                if(!isMainShrine)
                    _storeMoonFragmentsButton.SetActive(false);
                else
                    _storeMoonFragmentsButton.SetActive(true);

                playerHealthflaskBehaviour.RefillFlask();
                playerInfos.playerHealth = playerInfos.playerMaxHealth;
                playerInfos.respawnPos = transform.position;
            }
            if (sittingBehaviour.canSit)
            {
                try
                {
                    mainCamera.GetComponentInChildren<ChangeTargetBehaviour>().ChangeFollowTarget(sittingBehaviour.cameraLookAtPoint);
                    sittingBehaviour.benchBehaviour.IncreaseLightStrength();
                }
                catch
                {

                }
            }
        }
        else if (isInteracting)
        {
            isInteracting = false;
            mainCamera.GetComponentInChildren<ChangeTargetBehaviour>().ResetFollowTarget();
            try
            {
                sittingBehaviour.benchBehaviour.DecreaseLightStrength();
            }
            catch
            {

            }
        }
    }

    /// <summary>
    /// determines the direction the player last faced at before they stopped moving
    /// </summary>
    void DetermineDirectionState()
    {
        if (movementX == 0 && movementY > 0)        //Up
            directionState = 0;

        else if (movementX > 0 && movementY > 0)     //UpRight
            directionState = 1;

        else if (movementX > 0 && movementY == 0)    //Right
            directionState = 2;

        else if (movementX > 0 && movementY < 0)     //DownRight
            directionState = 3;

        else if (movementX == 0 && movementY < 0)    //Down
            directionState = 4;

        else if (movementX < 0 && movementY < 0)     //DownLeft
            directionState = 5;

        else if (movementX < 0 && movementY == 0)    //Left
            directionState = 6;

        else if (movementX < 0 && movementY > 0)     //UpLeft
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
    /// afterwards the trailRenderer stops emitting, isDashing is set to false, the players stamina is drained and applied to the staminaBar
    /// and the dashBufferTimer is reset to the dashBufferLength
    /// </summary>
    /// <returns>the duration the dash should last for</returns>
    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
        playerInfos.playerStamina -= playerInfos.dashStaminaRequirement;
        staminaBarBehaviour.FadingBarBehaviour();
        dashBufferTimer = dashBufferLength;
        gameObject.GetComponentInChildren<Collider2D>().enabled = true;
    }

    public void DecreaseWalkingSpeed()
    {
        speed = maxSpeed / 5;
    }

    public void ResetWalkingSpeed()
    {
        speed = maxSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shrine"))
        {
            shrineNearby = true;
            isMainShrine = collision.GetComponent<ShrineBehaviour>().mainShrine;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shrine"))
        {
            shrineNearby = false;
        }
    }
}
