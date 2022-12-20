using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour, IDataPersistence
{
    [SerializeField] Transform[] bossSpawnPoint;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] List<string> bossID;
    [SerializeField] bool bossDefeated;
    public List<GameObject> spawnedBosses;
    public bool canSpawnBosses;

    void Awake()
    {

    }

    /// <summary>
    /// sets the bool for the collider if it can spawn bosses
    /// </summary>
    void Start()
    {
        canSpawnBosses = true;
        bossDefeated = false;
    }

    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        for (int i = 0; i < bossID.Count; i++)
        {
            data.bossesDefeated.TryGetValue(bossID[i], out bossDefeated);
            if (bossDefeated)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < bossID.Count; i++)
        {
            if (data.bossesDefeated.ContainsKey(bossID[i]))
            {
                data.bossesDefeated.Remove(bossID[i]);
            }
            data.bossesDefeated.Add(bossID[i], bossDefeated);
        }
            
    }

    /// <summary>
    /// spawns in new bosses on each assigned spawning point
    /// </summary>
    public void SpawnBosses()
    {
        for (int i = 0; i < bossSpawnPoint.Length; i++)
        {
            GameObject newBoss = Instantiate(bossPrefab, bossSpawnPoint[i].position, Quaternion.identity);
            spawnedBosses.Add(newBoss);
            bossID.Add(spawnedBosses[i].GetComponent<BossInfos>().id);
        }
    }

    void BossDefeated()
    {
        bossDefeated = true;
    }

    /// <summary>
    /// if the player enters the collider the bosses are being spawned
    /// </summary>
    /// <param name="collision">used to check if the player is entering the collider</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canSpawnBosses && !bossDefeated)
        {
            if (collision.CompareTag("Player"))
            {
                SpawnBosses();
                canSpawnBosses = false;
            }
        }
    }
}