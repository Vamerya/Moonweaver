using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfos : MonoBehaviour, IDataPersistence
{
    
    [SerializeField] string id;
    [ContextMenu("Generate GUID for ID")]
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

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
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

    void EnemyTakeDamage(float dmg)
    {
        moonLightDamageHP.z -= dmg;
        
        if(moonLightDamageHP.z < 1)
        {
            AddMoonLight();
            isDead = true;
            Destroy(gameObject);
        }
    }

    IEnumerator EnemyTakeBurnDamage(float burnDmg)
    {
        while(isBurning)
        {
            moonLightDamageHP.z -= burnDmg;
            yield return new WaitForSecondsRealtime(.4f);
        }
    }

    void AddMoonLight()
    {
        playerLevelBehaviour.moonLight += moonLightDamageHP.x; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            EnemyTakeDamage(collision.GetComponent<PlayerWeaponBehaviour>().DamageEnemy());
            if(collision.GetComponentInParent<PlayerLevelBehaviour>().faith > 1)
            {
                isBurning = true;
                burningTimer = burningTimerInit;
                StartCoroutine(EnemyTakeBurnDamage(collision.GetComponent<PlayerWeaponBehaviour>().DetermineBurningDamage()));
            }
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
            Debug.Log("enemy hit");
        }
    }
}
