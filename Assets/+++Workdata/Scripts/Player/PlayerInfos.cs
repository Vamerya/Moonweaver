using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls all the infos of the player such as hp, stamina, ultCharge, whether they're alive, if they obtained their second weapon and more
/// </summary>
public class PlayerInfos : MonoBehaviour, IDataPersistence
{
    #region Variables
    [Header("Components")]
    PlayerSoundBehaviour playerSoundBehaviour;
    PlayerController playerController;
    PlayerCombat playerCombat;
    EnemyInfos enemyInfos;
    PlayerHealthflaskBehaviour playerHealthflaskBehaviour;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] StatBarBehaviour healthBarBehaviour;
    [SerializeField] PlayerDropMoonlight playerDropMoonlight;
    [SerializeField] ShrineManager shrineManager;
    [SerializeField] PlayerDisplayEquippedWeapon equippedWeapon;
    [SerializeField] GameObject companionPrefab;
    public List<GameObject> companions;

    [Header ("Timer")]
    [SerializeField] public float invincibilityTimer;
    [SerializeField] public float invincibilityTimerInit;

    [Header ("Player Stats")]
    public int playerLevel;
    public int collectedMoonFragments;
    public int inventoryState;
    public float playerMaxHealth;
    public float playerHealth;
    public float staminaRechargeSpeed;
    public float playerMaxStamina;
    public float playerStamina;
    public float dashStaminaRequirement;
    public float playerMaxUltCharg;
    public float playerUltCharge;
    public float playerUltDamage;
    public float playerHealthPercentage;
    public float playerStaminaPercentage;
    public Vector3 respawnPos;
    Vector3 startingPos;


    [Header ("Bools")]
    public bool isAlive;
    public bool obtainedSecondaryWeapon;
    public bool obtainedMoonFragment;
    public bool isDamaged;
    [SerializeField] bool swappedWeapon;
    #endregion


    /// <summary>
    /// grabs references from other scripts
    /// </summary>
    void Awake()
    {
        playerHealthflaskBehaviour = gameObject.GetComponent<PlayerHealthflaskBehaviour>();
        playerSoundBehaviour = gameObject.GetComponent<PlayerSoundBehaviour>();
        playerLevelBehaviour = gameObject.GetComponent<PlayerLevelBehaviour>();
        playerDropMoonlight = gameObject.GetComponent<PlayerDropMoonlight>();
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
    }
    /// <summary>
    /// saves the position the player starts in and sets current hp and stamina to their respective maxAmounts
    /// calculates the percentage of these stats
    /// </summary>
    void Start()
    {
        startingPos = transform.position;
        playerHealth = playerMaxHealth;
        playerStamina = playerMaxStamina;
        invincibilityTimer = invincibilityTimerInit;
        PlayerStatPercentage();
        SummonCompanions(companionPrefab);
    }

    /// <summary>
    /// loads data that has been saved
    /// loads default data if there's no saveFile
    /// destroys the rangedWeapon/firstMoonFragment in the scene if it was already picked up by the player
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(GameData data)
    {
        this.respawnPos = data.respawnPos;
        this.playerLevel = data.playerLevel;
        this.playerMaxHealth = data.playerHealth;
        this.playerMaxStamina = data.playerStamina;
        this.obtainedSecondaryWeapon = data.obtainedSecondaryWeapon;
        this.obtainedMoonFragment = data.obtainedMoonFragment;
        this.collectedMoonFragments = data.moonFragments;
        this.transform.position = data.playerPos;
        if(data.obtainedSecondaryWeapon)
        {
            Destroy(GameObject.FindGameObjectWithTag("WeaponPickup"));
        }

        if (data.obtainedMoonFragment)
        {
            Destroy(GameObject.FindGameObjectWithTag("FirstMoonFragment"));
        }

    }

    /// <summary>
    /// saves the data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        data.respawnPos = this.respawnPos;
        data.playerLevel = this.playerLevel;
        data.playerHealth = this.playerMaxHealth;
        data.playerStamina = this.playerMaxStamina;
        data.obtainedSecondaryWeapon = this.obtainedSecondaryWeapon;
        data.obtainedMoonFragment = this.obtainedMoonFragment;
        data.moonFragments = this.collectedMoonFragments;
        data.playerPos = this.transform.position;
    }

    /// <summary>
    /// sets bool of whether the player is alive or not
    /// if the player gets damaged a timer gets started to determine for how long the player is invincible, setting the isDamaged bool to false
    /// when it reaches 0
    /// while the players stamina is lower than the staminaMaxAmount, it gets increased by the given values
    /// </summary>
    void Update()
    {
        if(playerHealth > 0)
            isAlive = true;
            
        if(invincibilityTimer > 0)
            invincibilityTimer -= Time.deltaTime;
        else
            isDamaged = false;

        if(playerStamina < playerMaxStamina && !playerCombat.isAttacking)
            playerStamina += Time.deltaTime * staminaRechargeSpeed;

        if(playerStamina < 0)
            playerStamina = 0;

        PlayerStatPercentage();
    }

    /// <summary>
    /// manages the state of the currently equipped weapon and sets according parameter in animator
    /// </summary>
    public void SwapWeapon()
    {
        if(obtainedSecondaryWeapon)
            swappedWeapon = !swappedWeapon;

        if(swappedWeapon)
        {
            inventoryState = 1;
            equippedWeapon.SwapDisplayedWeapon();
        }
        else
        {
            inventoryState = 0;
            equippedWeapon.SwapDisplayedWeapon();
        }

        playerController.anim.SetInteger("inventoryState", inventoryState);
    }

    /// <summary>
    /// calculates player health with the damage values from the EnemyInfos.cs script
    /// if the player has less than 1 hp they're not alive anymore (duh...)
    /// </summary>
    /// <param name="dmg">passed in from the enemy the player touched</param>
    public void CalculatePlayerHealth(float dmg) 
    {
        if(isDamaged)
        {
            playerHealth -= dmg;
        }
            
        if(playerHealth < 1 && isAlive)
        {
            isAlive = false;
            playerDropMoonlight.DropMoonlight(playerLevelBehaviour.moonLight);
        }

        if(!isAlive)
            playerController.isInteracting = false;

        healthBarBehaviour.FadingBarBehaviour();
    }

    /// <summary>
    /// gives back a value from 0-1 for the player Health, Stamina and UltCharge
    /// </summary>
    void PlayerStatPercentage()
    {
        playerHealthPercentage = playerHealth / playerMaxHealth;
        playerStaminaPercentage = playerStamina / playerMaxStamina;
    }
    
    /// <summary>
    /// resets playerValues to the values that were saved beforehand
    /// </summary>
    public void ResetPlayer()
    {
        playerHealth = playerMaxHealth;
        transform.position = respawnPos;
        PlayerStatPercentage();
        healthBarBehaviour.SetStat(playerHealthPercentage);
        playerHealthflaskBehaviour.RefillFlask();
        shrineManager.RemoveAllEnemies();
        shrineManager.RemoveAllBosses();

        AkSoundEngine.SetState("GameplayMusicState", "Exploring");
    }

    /// <summary>
    /// enables combat after the player collected the first Moonfragment
    /// </summary>
    public void ObtainedFirstMoonFragment()
    {
        obtainedMoonFragment = true;
        playerCombat.enabled = true;
    }

    /// <summary>
    /// Summons companions based on the amount of collected Moonfragments
    /// </summary>
    /// <param name="_companion">GameObject that's being instantiated</param>
    void SummonCompanions(GameObject _companion)
    {
        for (int i = 0; i < collectedMoonFragments; i++)
        {
            GameObject newCompanion = Instantiate(_companion, this.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2)), Quaternion.identity);
            SetParent(this.transform, newCompanion);
            companions.Add(newCompanion);
        }
    }

    /// <summary>
    /// Removes all the spawned companions from the player and sets the amount of collected Moonfragments to 0
    /// </summary>
    public void RemoveCompanionsFromPlayer()
    {
        for (int i = 0; i < companions.Count; i = 0)
        {
            Destroy(companions[i]);
            companions.RemoveAt(i);
        }
        collectedMoonFragments = 0;
    }

    /// <summary>
    /// Sets new parent for the given child object
    /// </summary>
    /// <param name="_newParent">The new parent</param>
    /// <param name="_child">The child object which is assigned to the new parent</param>
    void SetParent(Transform _newParent, GameObject _child)
    {
        _child.transform.SetParent(_newParent);
    }
}
