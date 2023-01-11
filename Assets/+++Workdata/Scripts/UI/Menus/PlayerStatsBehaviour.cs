using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsBehaviour : MonoBehaviour
{
    public PlayerMeleeWeaponBehaviour playerMelee;
    public PlayerInfos playerInfos;
    [HideInInspector] public string statName;
    [HideInInspector] public string statValue;
    public string DetermineDisplayedStatName(int slotID)
    {
        switch(slotID)
        {
            case 0:
                statName = "HealthPoints";
                break;

            case 1:
                statName = "Stamina";
                break;

            case 2:
                statName = "Critical Strike Damage";
                break;

            case 3:
                statName = "Damage";
                break;

            case 4:
                statName = "Burn Damage";
                break;

            case 5:
                statName = "Critical Strike Chance";
                break;
        }
        
        return statName;
    }
    public string DetermineDisplayedStatValue(int slotID)
    {
        switch (slotID)
        {
            case 0:
                statValue = playerInfos.playerMaxHealth.ToString();
                break;

            case 1:
                statValue = playerInfos.playerMaxStamina.ToString();
                break;

            case 2:
                statValue = playerMelee.playerCriticalStrikeDamage.ToString();
                break;

            case 3:
                statValue = playerMelee.playerWeaponDamage.ToString();
                break;

            case 4:
                statValue = playerMelee.playerBurnDamage.ToString();
                break;

            case 5:
                double critChance = playerMelee.playerCriticalStrikeChance * 10;
                critChance = System.Math.Round(critChance, 2);
                statValue = critChance.ToString() + "%";
        break;
        }

        return statValue;
    }
}
