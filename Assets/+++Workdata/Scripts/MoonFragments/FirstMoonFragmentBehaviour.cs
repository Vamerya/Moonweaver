using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls how the first MoonFragment behaves
/// different things happen depending on if one of the IntroEnemies or the player reaches it first
/// </summary>
public class FirstMoonFragmentBehaviour : MonoBehaviour
{
    [SerializeField] GameObject companion;
    [SerializeField] GameObject[] introEnemies;    


    void Awake()
    {

    }

    /// <summary>
    /// puts all the IntroEnemies into an array
    /// </summary>
    void Start()
    {
        introEnemies = GameObject.FindGameObjectsWithTag("IntroEnemy");
    }

    /// <summary>
    /// if the player touches the MoonFragment the bool obtainedMoonFragment inside the player controller gets set to true
    /// a foreach-loop runs through the array, dealing damage to the enemies based on their maxHP to trigger their death
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            companion.SetActive(true);
            collision.GetComponentInParent<PlayerInfos>().ObtainedFirstMoonFragment();

            foreach(GameObject _introEnemy in introEnemies)
            {
                _introEnemy.GetComponent<EnemyInfos>().EnemyTakeDamage(_introEnemy.GetComponent<EnemyInfos>().moonLightDamageHP.z);
            }

            Destroy(gameObject);
            //ToDo - play transformation cutscene
        }

        if(collision.CompareTag("Enemy"))
        {
            //ToDo - transform enemy into boss, smacking Asters ass
        }
    }
}
