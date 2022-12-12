using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossInfos : MonoBehaviour
{
    [SerializeField] string bossName, bossTitle;
    [SerializeField] BossBehaviour bossBehaviour;
    [SerializeField] TextMeshProUGUI nameAndTitle;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] GameObject moonFragment;
    [SerializeField] float burningTimer, burningTimerInit;
    [SerializeField] float knockbackForce = 10;
    Color mainColor;
    public float bossMoonLight;
    public float bossDamage;
    public float bossHealth;
    public float bossMaxHealth;
    public float bossHealthPercentage;
    public bool isDead;
    public bool isBurning;

    /// <summary>
    /// grabs references of necessary components
    /// </summary>
    void Awake()
    {
        bossBehaviour = gameObject.GetComponent<BossBehaviour>();
        nameAndTitle = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        playerLevelBehaviour = GameObject.FindObjectOfType<PlayerLevelBehaviour>();
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
    }

    /// <summary>
    /// counts down the timer for how long th enemy is burning
    /// changes the sprite color for the duration the enemy is burning
    /// </summary>
    void Update()
    {
        if (burningTimer > 0)
            burningTimer -= Time.deltaTime;
        else
            isBurning = false;

        if (isBurning)
            bossBehaviour.spriteRenderer.color = Color.yellow;
        else
            bossBehaviour.spriteRenderer.color = mainColor;
    }

    /// <summary>
    /// determines the type of enemy based on their ID
    /// </summary>
    /// <returns>returns a Vector3 with the enemys stats for Moonlight carried, their Damage and total HP</returns>
    public void DetermineBossValues()
    {
        
    }

    /// <summary>
    /// recalculates the enemys health
    /// adds Moonlight to the player if their hp is below 1, sets their isDead bool to true and destroys the gameObject afterwards
    /// </summary>
    /// <param name="dmg">value which is used to input the amount of damage the player dealt</param>
    public void BossTakeDamage(float dmg)
    {
        bossHealth -= dmg;
        DetermineBossHealthPercentage();

        if (bossHealth < 1)
        {
            AddMoonLight();
            GameObject droppedMoonFragment = Instantiate(moonFragment, transform.position, Quaternion.identity);
            isDead = true;
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
    /// calls the EnemyTakeDamage upon colliding with either the players melee weapon or one of the projectiles, grabbing references to either 
    /// weapons behaviour in order to input the damage
    /// if the players faith level is above 1 the enemy is also burning with the amount of damage input from the respective function
    /// </summary>
    /// <param name="collision">checks what the enemy was hit with and grabs references to specific scripts from that collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            bossBehaviour.rb.AddForce((bossBehaviour.aiPath.desiredVelocity * -1) * knockbackForce, ForceMode2D.Force);
            BossTakeDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DamageEnemyMelee());
            if (collision.GetComponentInParent<PlayerLevelBehaviour>().faith > 1)
            {
                isBurning = true;
                burningTimer = burningTimerInit;
                StartCoroutine(BossTakeBurnDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineBurningDamage()));
            }
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }

        if (collision.CompareTag("Bullet"))
        {
            BossTakeDamage(collision.GetComponent<ProjectileBehaviour>().DamageEnemyRanged());
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }

    void DetermineBossHealthPercentage()
    {
        bossHealthPercentage = bossHealth / bossMaxHealth;
    }
}
