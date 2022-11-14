using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls how many healthFlasks the player has, how much hp gets replenished per use and when they fill back up again 
/// </summary>
public class PlayerHealthflaskBehaviour : MonoBehaviour, IDataPersistence
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] DisplayMoonwaterAmount moonwaterAmount;
    [SerializeField] float healAmount;
    public int maxAmount, amountAvailable;

    /// <summary>
    /// grabs reference to the playerInfos script
    /// </summary>
    void Awake()
    {
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        moonwaterAmount.UpdateMoonWaterAmount();
    }

    /// <summary>
    /// loads saved data
    /// loads default data if there's no saved data
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(GameData data)
    {
        this.amountAvailable = data.moonwater;
    }

    /// <summary>
    /// saves data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        data.moonwater = this.amountAvailable;
    }

    /// <summary>
    /// if the player has more than 0 flasks available and less hp than their maxHP the available amount gets decreased by one, and the player is healed
    /// by the determined amount of hp
    /// afterwards the available amount of flasks is updated
    /// </summary>
    public void UseFlask()
    {
        if(amountAvailable > 0 && playerInfos.playerHealth < playerInfos.playerMaxHealth)
        {
            amountAvailable -= 1;
            float amountToHeal = playerInfos.playerMaxHealth * healAmount;
            amountToHeal = Mathf.RoundToInt(amountToHeal);
            playerInfos.playerHealth += amountToHeal;
            if(playerInfos.playerHealth > playerInfos.playerMaxHealth)
            {
                playerInfos.playerHealth = playerInfos.playerMaxHealth;
            }
            moonwaterAmount.UpdateMoonWaterAmount();
        }
    }

    /// <summary>
    /// refills the flasks back to its maxAmount
    /// </summary>
    public void RefillFlask()
    {
        amountAvailable = maxAmount;
        moonwaterAmount.UpdateMoonWaterAmount();
    }

}
