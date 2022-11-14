using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// saves all the infos for the enemies
/// </summary>
public class EnemyInfos : MonoBehaviour, IDataPersistence
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

    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] float burningTimer, burningTimerInit;

    SpriteRenderer spriteRenderer;
    Color mainColor;
    public Vector3 moonLightDamageHP;
    public bool isDead;
    public bool isBurning;

    /// <summary>
    /// grabs references of necessary components
    /// </summary>
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// sets isDead to false when the game starts
    /// saves the maincolor based on the current color
    /// </summary>
    void Start()
    {
        isDead = false;
        mainColor = spriteRenderer.color;
    }

    public void LoadData(GameData data)
    {
        data.enemiesDefeated.TryGetValue(id, out isDead);
        if(isDead)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if(data.enemiesDefeated.ContainsKey(id))
        {
            data.enemiesDefeated.Remove(id);
        }
        data.enemiesDefeated.Add(id, isDead);
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
            spriteRenderer.color = Color.yellow;
        else
            spriteRenderer.color = mainColor;
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
                moonLightDamageHP = new Vector3(Random.Range(400, 600), Random.Range(80, 120), 1000); //normal melee add
                break;
            case 1:
                moonLightDamageHP = new Vector3(Random.Range(400, 600), Random.Range(180, 220), 500); //normal ranged add
                break;
            case 2:
                moonLightDamageHP = new Vector3(Random.Range(800, 1200), Random.Range(80, 120), 1500); //tanky enemy
                break;
            case 3:
                moonLightDamageHP = new Vector3(Random.Range(1300, 1700), Random.Range(800, 1200), 500); //assassin
                break;
            case 4:
                moonLightDamageHP = new Vector3(500, 100, 1000); //open slot
                break;
            case 5:
                moonLightDamageHP = new Vector3(500, 100, 1000); //open slot
                break;
            default:
                moonLightDamageHP = new Vector3(10000, 1000, 5000); //Boss type
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
    /// <returns>waits for .4 seconds before returning</returns>
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
    /// calls the EnemyTakeDamage upon colliding with either the players melee weapon or one of the projectiles, grabbing references to either 
    /// weapons behaviour in order to input the damage
    /// if the players faith level is above 1 the enemy is also burning with the amount of damage input from the respective function
    /// </summary>
    /// <param name="collision">checks what the enemy was hit with and grabs references to specific scripts from that collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            EnemyTakeDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DamageEnemyMelee());
            if(collision.GetComponentInParent<PlayerLevelBehaviour>().faith > 1)
            {
                isBurning = true;
                burningTimer = burningTimerInit;
                StartCoroutine(EnemyTakeBurnDamage(collision.GetComponent<PlayerMeleeWeaponBehaviour>().DetermineBurningDamage()));
            }
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
        
        if(collision.CompareTag("Bullet"))
        {
            EnemyTakeDamage(collision.GetComponent<ProjectileBehaviour>().DamageEnemyRanged());
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }
}
