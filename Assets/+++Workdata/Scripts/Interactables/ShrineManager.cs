using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShrineManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] public PlayerController playerController;
    public GameObject _shrineMenu;
    public GameObject _levelUpMenu;
    public GameObject _statMenu;
    [SerializeField] public TextMeshProUGUI _areaName;
    [SerializeField] GameObject _playerBars;
    [SerializeField] GameObject _playerHeldMoonlight;
    [SerializeField] Transform mainShrine;
    [SerializeField] Vector3 offset;
    [SerializeField] EnemySpawner[] enemySpawner;
    [SerializeField] BossSpawner[] bossSpawner;
    [SerializeField] GameObject moonFragment;

    [SerializeField] public float storedMoonFragments;
    [SerializeField] List<GameObject> moonFragments;


    /// <summary>
    /// loads saved data
    /// loads default data if there is no saved data available
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(GameData data)
    {
        this.storedMoonFragments = data.storedMoonFragments;
    }

    /// <summary>
    /// saves the data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        data.storedMoonFragments = this.storedMoonFragments;
    }

    /// <summary>
    /// puts all the enemy spawners into an array
    /// </summary>
    void Awake()
    {
        enemySpawner = GameObject.FindObjectsOfType<EnemySpawner>();
        bossSpawner = GameObject.FindObjectsOfType<BossSpawner>();
    }

    /// <summary>
    /// Instantiates MoonFragments based on the stored amount
    /// </summary>
    void Start()
    {
        InstantiateMoonFragments(storedMoonFragments);
    }

    /// <summary>
    /// displays the level up UI and re-enables all the enemy spawners again 
    /// </summary>
    public void ShowShrineMenu()
    {
        _shrineMenu.SetActive(true);
        _playerBars.SetActive(false);
        _playerHeldMoonlight.SetActive(false);
        playerController.inGameInputActions.Disable();
        RemoveAllEnemies();
        RemoveAllBosses();
    }

    /// <summary>
    /// hides the level up UI
    /// </summary>
    public void HideShrineMenu()
    {
        _shrineMenu.SetActive(false);
        _levelUpMenu.SetActive(false);
        _statMenu.SetActive(false);

        _playerBars.SetActive(true);
        _playerHeldMoonlight.SetActive(true);
    }

    /// <summary>
    /// Stores the collected MoonFragments and also sets the follow target for mentioned fragments to the shrine
    /// </summary>
    public void StoreMoonFragments()
    {
        storedMoonFragments += playerInfos.collectedMoonFragments;
        InstantiateMoonFragments(playerInfos.collectedMoonFragments);
        playerInfos.RemoveCompanionsFromPlayer();
        SetTargetForMoonFragments(mainShrine);
    }

    /// <summary>
    /// Sets follow target for each fragment in the list to the target
    /// </summary>
    /// <param name="target">Used to determine what the gameobject should be targeting</param>
    public void SetTargetForMoonFragments(Transform target)
    {
        foreach (GameObject fragment in moonFragments)
        {
            fragment.GetComponent<CompanionBehaviour>().target = target;
        }
    }

    /// <summary>
    /// Removes all normal enemies from each spawning zone
    /// </summary>
    public void RemoveAllEnemies()
    {
        for (int i = 0; i < enemySpawner.Length; i++)
        {
            for (int j = 0; j < enemySpawner[i].spawnedEnemies.Count; j = 0)
            {
                Destroy(enemySpawner[i].spawnedEnemies[j]);
                enemySpawner[i].spawnedEnemies.RemoveAt(0);
            }
            enemySpawner[i].canSpawnEnemies = true;
        }
    }

    /// <summary>
    /// Removes every spawned boss
    /// </summary>
    public void RemoveAllBosses()
    {
        for (int i = 0; i < bossSpawner.Length; i++)
        {
            for (int j = 0; j < bossSpawner[i].spawnedBosses.Count; j = 0)
            {
                Destroy(bossSpawner[i].spawnedBosses[j]);
                bossSpawner[i].spawnedBosses.RemoveAt(0);
            }
            bossSpawner[i].canSpawnBosses = true;
        }
    }

    /// <summary>
    /// Sets new parent 
    /// </summary>
    /// <param name="_parent">This is the new parent</param>
    /// <param name="_child">This is the childobject which gets a new parent</param>
    void SetParent(Transform _parent, GameObject _child)
    {
        _child.transform.SetParent(_parent);
    }

    /// <summary>
    /// Instantiates new MoonFragments based on the amount of stored fragments
    /// </summary>
    /// <param name="fragmentAmount">Used to determine how many fragments should be instantiated</param>
    void InstantiateMoonFragments(float fragmentAmount)
    {
        for (int i = 0; i < fragmentAmount; i++)
        {
            GameObject _moonFragment = Instantiate(moonFragment, mainShrine.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2)), Quaternion.identity, moonFragment.GetComponent<CompanionBehaviour>().target = mainShrine);
            moonFragments.Add(_moonFragment);
            SetParent(mainShrine, moonFragments[i]);
        }
    }
}
