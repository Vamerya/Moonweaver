using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] PlayerHealthflaskBehaviour healthflaskBehaviour;
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
    // - Mind -> Execute @ specific hp% || bleed build-up
    // - Strength -> do stuff but heavy
    // - Dexterity -> do stuff but fast
    // - Faith -> radiant/burning damage
    // - Luck -> crit damage
    #endregion

    void Awake() 
    {

    }

    void Start()
    {

    }

    public void DetermineStatToLevel(int stat)
    {
        switch (stat)
        {
            case 0:
                playerLevelBehaviour.IncreasePlayerHP();
                statName = "Vigor";
                currentLevel = playerLevelBehaviour.vigor;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            case 1:
                playerLevelBehaviour.IncreasePlayerEndurance();
                statName = "Endurance";
                currentLevel = playerLevelBehaviour.endurance;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            case 2:
                playerLevelBehaviour.IncreasePlayerMind();
                statName = "Mind";
                currentLevel = playerLevelBehaviour.mind;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            case 3:
                playerLevelBehaviour.IncreasePlayerStrength();
                statName = "Strength";
                currentLevel = playerLevelBehaviour.strength;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            case 4:
                playerLevelBehaviour.IncreasePlayerDexterity();
                statName = "Dexterity";
                currentLevel = playerLevelBehaviour.dexterity;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            case 5:
                playerLevelBehaviour.IncreasePlayerFaith();
                statName = "Faith";
                currentLevel = playerLevelBehaviour.faith;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            case 6:
                playerLevelBehaviour.IncreasePlayerLuck();
                statName = "Luck";
                currentLevel = playerLevelBehaviour.luck;
                playerLevelBehaviour.RequiredMoonlightAfterLevelUp(playerInfos.playerLevel);
                break;
            default:
                Debug.Log("No Stat ID");
                break;
        }
    }
    public void LevelUpInitiator(int stat)
    {
        switch (stat)
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
