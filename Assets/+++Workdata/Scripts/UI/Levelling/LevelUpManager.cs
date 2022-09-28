using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public LevelUpSlot[] levelUpSlot;
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] GameObject _levelUpUI;
    [SerializeField] GameObject _requiredRunesText;
    [SerializeField] GameObject _errorMessage;
    public string statName;
    public int currentLevel;
    public int levelUpMenuState = 0;
    #region Stats
    // - Vigor -> Overall HP
    // - Endurance -> Overall stamina
    // - Mind -> Ult Recharge
    // - Strength -> Overall damage
    // - Dexterity -> Invincibility time
    // - Faith -> Increased spell damage (ult for now)
    // - Luck -> increased item drop chance/runes
    #endregion

    void Awake() 
    {
        //playerLevelBehaviour = gameObject.GetComponent<PlayerLevelBehaviour>();
    }

    void Start()
    {

    }

    public void DetermineStatToLevel(int stat)
    {
        for(int i = 0; i < levelUpSlot.Length; i++)
        {
            switch(stat)
            {
                case 0:
                    playerLevelBehaviour.IncreasePlayerHP();
                    statName = "Vigor";
                    currentLevel = playerLevelBehaviour.vigor;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                case 1:
                    playerLevelBehaviour.IncreasePlayerEndurance();
                    statName = "Endurance";
                    currentLevel = playerLevelBehaviour.endurance;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                case 2:
                    playerLevelBehaviour.IncreasePlayerMind();
                    statName = "Mind";
                    currentLevel = playerLevelBehaviour.mind;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                case 3:
                    playerLevelBehaviour.IncreasePlayerStrength();
                    statName = "Strength";
                    currentLevel = playerLevelBehaviour.strength;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                case 4:
                    playerLevelBehaviour.IncreasePlayerDexterity();
                    statName = "Dexterity";
                    currentLevel = playerLevelBehaviour.dexterity;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                case 5:
                    playerLevelBehaviour.IncreasePlayerFaith();
                    statName = "Faith";
                    currentLevel = playerLevelBehaviour.faith;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                case 6:
                    playerLevelBehaviour.IncreasePlayerLuck();
                    statName = "Luck";
                    currentLevel = playerLevelBehaviour.luck;
                    playerLevelBehaviour.RequiredRunes(playerInfos.playerLevel);
                break;
                default:
                    Debug.Log("No Stat ID");
                break;
            }
        }
    }
    public void LevelUpInitiator(int stat)
    {
        for(int i = 0; i < levelUpSlot.Length; i++)
        {
            switch(stat)
            {
                case 0:
                    statName = "Vigor";
                    currentLevel = playerLevelBehaviour.vigor;
                break;
                case 1:
                    statName = "Endurance";
                    currentLevel = playerLevelBehaviour.endurance;
                break;
                case 2:
                    statName = "Mind";
                    currentLevel = playerLevelBehaviour.mind;
                break;
                case 3:
                    statName = "Strength";
                    currentLevel = playerLevelBehaviour.strength;
                break;
                case 4:
                    statName = "Dexterity";
                    currentLevel = playerLevelBehaviour.dexterity;
                break;
                case 5:
                    statName = "Faith";
                    currentLevel = playerLevelBehaviour.faith;
                break;
                case 6:
                    statName = "Luck";
                    currentLevel = playerLevelBehaviour.luck;
                break;
                default:
                    Debug.Log("No Stat ID");
                break;
            }
        }
    }

    public void ToggleLevelUpUI()
    {
        Debug.Log(levelUpMenuState.ToString());
        
        if(levelUpMenuState == 0)
        {
            levelUpMenuState = 1;
            _levelUpUI.SetActive(true);
            playerController._playerHotbar.SetActive(false);
            playerController._playerInventory.SetActive(false);
            Time.timeScale = 0f;
        }
        else if(levelUpMenuState == 1)
        {
            levelUpMenuState = 0;
            _levelUpUI.SetActive(false);
            playerController._playerHotbar.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}
