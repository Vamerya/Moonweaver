using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// saves all the infos for the enemies
/// </summary>
public class EnemyInfos : MonoBehaviour
{
    /// <summary>
    /// saves the enemys ID
    /// </summary>
    [SerializeField] string id;
    [ContextMenu("Generate GUID for ID")]
    
    /// <summary>
    /// generates a unique ID for each enemy
    /// </summary>
    void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] EnemyBehaviour enemyBehaviour;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] float burningTimer, burningTimerInit;
    [SerializeField] float knockbackForce = 10;
    Color mainColor;
    public Vector3 moonLightDamageHP;
    public float maxHP;
    public bool isDead;
    public bool isBurning;

    /// <summary>
    /// grabs references of necessary components
    /// </summary>
    void Awake()
    {
        enemyBehaviour = gameObject.GetComponent<EnemyBehaviour>();
        playerLevelBehaviour = GameObject.FindObjectOfType<PlayerLevelBehaviour>();
    }
    /// <summary>
    /// sets isDead to false when the game starts
    /// saves the maincolor based on the current color
    /// </summary>
    void Start()
    {
        maxHP = moonLightDamageHP.z;
        isDead = false;
        mainColor = enemyBehaviour.spriteRenderer.color;
    }

    /// <summary>
    /// counts down the timer for how long th enemy is burning
    /// changes the sprite color for the duration the enemy is burning
    /// </summary>
    void Update()
    {
        if(burningTimer > 0)
            burningTimer -= Time.deltaTime;
        else
            isBurning = false;

        if(isBurning)
            enemyBehaviour.spriteRenderer.color = Color.blue;
        else
            enemyBehaviour.spriteRenderer.color = mainColor;
    }

    /// <summary>
    /// determines the type of enemy based on their ID
    /// </summary>
    /// <param name="ID">the id of the enemy is used to determine which case to use</param>
    /// <returns>returns a Vector3 with the enemys stats for Moonlight carried, their Damage and total HP</returns>
    public Vector3 DetermineEnemyType(int ID)
    {
        switch(ID)
        {
            case 0:
                moonLightDamageHP = new Vector3(Random.Range(20, 40), Random.Range(55, 90), 800); //normal melee add
                break;
            case 1:
                moonLightDamageHP = new Vector3(Random.Range(40, 100), Random.Range(180, 220), 500); //normal ranged add
                break;
            case 2:
                moonLightDamageHP = new Vector3(Random.Range(100, 250), Random.Range(80, 120), 1500); //tanky enemy
                break;
            case 3:
                moonLightDamageHP = new Vector3(Random.Range(1300, 1700), Random.Range(800, 1200), 300); //assassin
                break;
            case 4:
                moonLightDamageHP = new Vector3(500, 100, 1000); //open slot
                break;
            case 5:
                moonLightDamageHP = new Vector3(10000, 1000, 5000); //BossType
                break;
            default:
                moonLightDamageHP = new Vector3(Random.Range(400, 600), 1000, 5000); //IntroEnemies
                break;

        }

        return moonLightDamageHP;
    }

    /// <summary>
    /// recalculates the enemys health
    /// adds Moonlight to the player if their hp is below 1, sets their isDead bool to true and destroys the gameObject afterwards
    /// </summary>
    /// <param name="dmg">value which is used to input the amount of damage the player dealt</param>
    public void EnemyTakeDamage(float dmg)
    {
        moonLightDamageHP.z -= dmg;
        
        if(moonLightDamageHP.z < 1)
        {
            AddMoonLight();
            isDead = true;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// periodically removes hp from the enemy while its burning
    /// </summary>
    /// <param name="burnDmg">damage determined by the players Faith level</param>
    /// <returns>the amount of time that should pass between each call</returns>
    IEnumerator EnemyTakeBurnDamage(float burnDmg)
    {
        while(isBurning)
        {
            moonLightDamageHP.z -= burnDmg;
            yield return new WaitForSecondsRealtime(.4f);
        }
    }

    /// <summary>
    /// adds the amount of Moonlight the enemy carried to the total amount of Moonlight the player has
    /// </summary>
    void AddMoonLight()
    {
        playerLevelBehaviour.moonLight += moonLightDamageHP.x; 
    }

    /// <summary>
    /// calls the EnemyTakeDamage upon colliding with the players melee weapon, grabbing references to either 
    /// weapons behaviour in order to input the damage
    /// if the players faith level is above 1 the enemy is also burning with the amount of damage input from the respective function
    /// </summary>
    /// <param name="collision">checks what the enemy was hit with and grabs references to specific scripts from that collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            EnemyKnockback();
            EnemyTakeDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DamageEnemyMelee());
            if(collision.GetComponentInParent<PlayerLevelBehaviour>().faith > 1)
            {
                isBurning = true;
                burningTimer = burningTimerInit;
                StartCoroutine(EnemyTakeBurnDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineBurningDamage()));
            }
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }

    IEnumerator EnemyKnockback()
    {
        var velo = enemyBehaviour.aiPath.maxSpeed;
        enemyBehaviour.aiPath.maxSpeed = 0;
        enemyBehaviour.rb.AddForce((enemyBehaviour.aiPath.desiredVelocity * -1) * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(.3f);
        enemyBehaviour.aiPath.maxSpeed = velo;
        //Debug.LogError(enemyBehaviour.rb.velocity);
    }
}