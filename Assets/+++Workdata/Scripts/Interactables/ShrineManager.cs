using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineManager : MonoBehaviour
{
    [SerializeField] EnemySpawner[] enemySpawner;
    public GameObject _levelUpUI;
    public PlayerController playerController;

    /// <summary>
    /// puts all the enemy spawners into an array
    /// </summary>
    void Awake()
    {
        enemySpawner = GameObject.FindObjectsOfType<EnemySpawner>();
    }
    void Start()
    {
        
    }

    /// <summary>
    /// displays the level up UI and re-enables all the enemy spawners again 
    /// </summary>
    public void ShowLevelUpUI()
    {
        _levelUpUI.SetActive(true);
        for (int i = 0; i < enemySpawner.Length; i++)
        {
            enemySpawner[i].canSpawnEnemies = true;
        }
    }

    /// <summary>
    /// hides the level up UI
    /// </summary>
    public void HideLevelUpUI()
    {
        _levelUpUI.SetActive(false);
    }
}
