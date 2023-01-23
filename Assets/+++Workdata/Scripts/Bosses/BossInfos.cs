using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossInfos : MonoBehaviour, IDataPersistence
{
    /// <summary>
    /// saves the boss ID
    /// </summary>
    public string id;
    [ContextMenu("Generate GUID for ID")]

    /// <summary>
    /// generates a unique ID for each boss
    /// </summary>
    void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] string bossName, bossTitle;
    [SerializeField] string akEventName;
    [SerializeField] BossBehaviour bossBehaviour;
    [SerializeField] BossHealthBarBehaviour bossHealthBar;
    [SerializeField] TextMeshProUGUI nameAndTitle;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] GameObject moonFragment;
    [SerializeField] Color burnColor;
    [SerializeField] float burningTimer, burningTimerInit;
    Color mainColor;
    public float bossMoonLight;
    public float bossDamage;
    public float bossHealth;
    public float bossMaxHealth;
    public float bossHealthPercentage;
    public bool isDead;
    public bool isBurning;

    [Header ("Determine the knockback resistance as a fraction of 1")]
    [SerializeField] float knockbackResistance;
    bool knockedBack;
    [SerializeField] float knockbackDistance, knockBackSpeed;
    Vector3 knockbackPos;

    public float aiVelo;

    /// <summary>
    /// grabs references of necessary components
    /// </summary>
    void Awake()
    {
        bossBehaviour = gameObject.GetComponent<BossBehaviour>();
        bossHealthBar = gameObject.GetComponentInChildren<BossHealthBarBehaviour>();
        nameAndTitle = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        playerLevelBehaviour = GameObject.FindObjectOfType<PlayerLevelBehaviour>();

        //AkSoundEngine.PostEvent(akEventName, this.gameObject);

        AkSoundEngine.SetState("GameplayMusicState", "Boss");
    }
    /// <summary>
    /// sets isDead to false when the game starts
    /// saves the maincolor based on the current color
    /// </summary>
    void Start()
    {
        bossMaxHealth = bossHealth;
        isDead = false;
        mainColor = bossBehaviour.spriteRenderer.color;
        nameAndTitle.text = bossName + ", " + bossTitle;
        DetermineBossHealthPercentage();

        aiVelo = bossBehaviour.aiPath.maxSpeed;
    }

    public void LoadData(GameData data)
    {
        data.bossesDefeated.TryGetValue(id, out isDead);
        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.bossesDefeated.ContainsKey(id))
        {
            data.bossesDefeated.Remove(id);
        }
        data.bossesDefeated.Add(id, isDead);
    }


    /// <summary>
    /// counts down the timer for how long the boss is burning
    /// changes the sprite color for the duration the boss is burning
    /// </summary>
    void Update()
    {
        if (burningTimer > 0)
            burningTimer -= Time.deltaTime;
        else
            isBurning = false;

        if (isBurning)
            bossBehaviour.spriteRenderer.color = burnColor;
        else
            bossBehaviour.spriteRenderer.color = mainColor;
    }

    void FixedUpdate()
    {
        if (knockedBack)
        {

            transform.position = Vector3.MoveTowards(transform.position, knockbackPos, knockBackSpeed * Time.deltaTime);

        }
        
        if (Vector3.Distance(transform.position, knockbackPos) < .2f)
        {
            Debug.Log("Stop Knockback");
            knockedBack = false;
            AiPathFinding(true);
        }
    }

    /// <summary>
    /// recalculates the boss health
    /// adds Moonlight to the player if their hp is below 1, sets their isDead bool to true and destroys the gameObject afterwards
    /// </summary>
    /// <param name="dmg">value which is used to input the amount of damage the player dealt</param>
    public void BossTakeDamage(float dmg)
    {
        bossHealth -= dmg;
        DetermineBossHealthPercentage();
        bossHealthBar.FadingBarBehaviour();

        if (bossHealth < 1)
        {
            AddMoonLight();
            GameObject droppedMoonFragment = Instantiate(moonFragment, transform.position, Quaternion.identity);
            isDead = true;
            AkSoundEngine.SetState("GameplayMusicState", "Exploring");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// periodically removes hp from the enemy while its burning
    /// </summary>
    /// <param name="burnDmg">damage determined by the players Faith level</param>
    /// <returns>the amount of time that should pass between each call</returns>
    IEnumerator BossTakeBurnDamage(float burnDmg)
    {
        while (isBurning)
        {
            bossHealth -= burnDmg;
            DetermineBossHealthPercentage();
            bossHealthBar.FadingBarBehaviour();
            yield return new WaitForSecondsRealtime(.4f);
        }
    }

    /// <summary>
    /// adds the amount of Moonlight the enemy carried to the total amount of Moonlight the player has
    /// </summary>
    void AddMoonLight()
    {
        playerLevelBehaviour.moonLight += bossMoonLight;
    }

    /// <summary>
    /// calls the  BossTakeDamage upon colliding with either the players melee weapon or one of the projectiles, grabbing references to either 
    /// weapons behaviour in order to input the damage
    /// if the players faith level is above 1 the enemy is also burning with the amount of damage input from the respective function
    /// </summary>
    /// <param name="collision">checks what the enemy was hit with and grabs references to specific scripts from that collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            knockbackDistance = collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineKnockbackDistance() * (1 - knockbackResistance);
            knockBackSpeed = collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineKnockbackSpeed();
            CalculateKnockbackPos(collision.transform);
            BossTakeDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DamageEnemyMelee());
            if (collision.GetComponentInParent<PlayerLevelBehaviour>().faith > 1)
            {
                isBurning = true;
                burningTimer = burningTimerInit;
                StartCoroutine(BossTakeBurnDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineBurningDamage()));
            }
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }

    void DetermineBossHealthPercentage()
    {
        bossHealthPercentage = bossHealth / bossMaxHealth;
    }

    void AiPathFinding(bool value)
    {
        if (!value)
        {
            bossBehaviour.aiPath.maxSpeed = 0;
            bossBehaviour.aiPath.enabled = false;
        }
        else
        {
            bossBehaviour.aiPath.enabled = true;
            bossBehaviour.aiPath.maxSpeed = aiVelo;
        }
    }

    void CalculateKnockbackPos(Transform player)
    {
        Vector3 a = player.position;
        Vector3 b = transform.position;
        Vector3 c = knockbackPos;
        Vector3 dir = a - b;
        c = b;

        c = c - dir;

        float distance = Vector3.Distance(c, b);
        Vector3 fromOriginToObject = c - b;
        fromOriginToObject *= knockbackDistance / distance;
        c = b + fromOriginToObject;
        knockbackPos = c;

        AiPathFinding(false);
        knockedBack = true;

    }
}
