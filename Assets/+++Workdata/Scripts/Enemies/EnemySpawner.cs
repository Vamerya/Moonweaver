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
    public bool canSpawnEnemies;

    void Awake()
    {

    }

    /// <summary>
    /// sets the bool for the collider if it can spawn enemies to try
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
            Debug.Log("KSKSKS");
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPoint[i].position, Quaternion.identity);
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
