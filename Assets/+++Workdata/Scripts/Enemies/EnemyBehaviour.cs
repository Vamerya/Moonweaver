using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    public EnemyInfos enemyInfos;


    public int enemyID;

    void Awake()
    {
        enemyInfos = gameObject.GetComponent<EnemyInfos>();
    }
    void Start()
    {
        enemyInfos.DetermineEnemyType(enemyID);
    }

    void Update()
    {
      
    }
}
