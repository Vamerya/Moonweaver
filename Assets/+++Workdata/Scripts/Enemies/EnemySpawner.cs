using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls how, when and which enemies are spawned from their respective spawning points
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPoint;
    [SerializeField] GameObject enemyPrefab;
    public List<GameObject> spawnedEnemies;
    public bool canSpawnEnemies;

    void Awake()
    {

    }

    /// <summary>
    /// sets the bool for the collider if it can spawn enemies
    /// </summary>
    void Start()
    {
        canSpawnEnemies = true;
    }

    void Update()
    {

    }

    /// <summary>
    /// spawns in new enemies on each assigned spawning point
    /// </summary>
    public void SpawnEnemies()
    {   
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPoint[i].position, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }
    }

    /// <summary>
    /// if the player enters the collider the enemies are being spawned
    /// </summary>
    /// <param name="collision">used to check if the player is entering the collider</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(canSpawnEnemies)
        {
            if(collision.CompareTag("Player"))
            {
                SpawnEnemies();
                canSpawnEnemies = false;
            }
        }
    }
}
