using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSlot : MonoBehaviour
{
    [Header ("Components")]
    public PlayerInfos playerInfos;
    public PlayerLevelBehaviour playerLevelBehaviour;
    public LevelUpManager levelUpManager;
    public Image statIcon;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI statLevel;
    public int slotID;
    float timer;


    void Awake()
    {

    }

    void Start()
    {
        levelUpManager.LevelUpInitiator(slotID);
        statText.text = levelUpManager.statName.ToString() + ": ";
        statLevel.text = levelUpManager.currentLevel.ToString();
    }

    void Update()
    {

    }

    public void InitiateLevelUP()
    {
        if(playerLevelBehaviour.levelUpReady)
        {
            levelUpManager.DetermineStatToLevel(slotID);
        
            statText.text = levelUpManager.statName.ToString() + ": ";
            statLevel.text = levelUpManager.currentLevel.ToString();
        }    
    }
}
