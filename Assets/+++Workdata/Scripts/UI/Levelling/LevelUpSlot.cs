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
    public TextMeshProUGUI errorMessage, randomMessage;
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

        randomMessage.text = playerLevelBehaviour.requiredMoonLight.ToString() + " Moonlight is required for the next level up";
        errorMessage.text = "You have " + playerLevelBehaviour.moonLight.ToString() + " Moonlight available";

    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
            errorMessage.text = "You have " + playerLevelBehaviour.moonLight.ToString() + " Moonlight available";
    }

    public void InitiateLevelUP()
    {
        if(playerLevelBehaviour.levelUpReady)
        {
            levelUpManager.DetermineStatToLevel(slotID);
        
            statText.text = levelUpManager.statName.ToString() + ": ";
            statLevel.text = levelUpManager.currentLevel.ToString();
            randomMessage.text = playerLevelBehaviour.requiredMoonLight.ToString() + " Moonlight is required for the next level up";
        }
        else
        {
            errorMessage.text = "Not enough Moonlight";
            timer = 4f;        
        }    
    }
}
