using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelBehaviour : MonoBehaviour
{
    PlayerController playerController;
    PlayerCombat playerCombat;
    PlayerInfos playerInfos;

    float level, playerHealthIncrease, staminaIncrease, collectedMoonFragments;
    public float requiredRunes, runes;
    bool levelUpReady;


    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void LevelUp()
    {
        
    }

    void Experience()
    {

    }
}
