using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls how many healthFlasks the player has, how much hp gets replenished per use and when they fill back up again 
/// </summary>
public class PlayerHealthflaskBehaviour : MonoBehaviour, IDataPersistence
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] PlayerController playerController;
    [SerializeField] DisplayMoonwaterAmount moonwaterAmount;
    [SerializeField] float healAmount;
    [SerializeField] bool isHealing;
    [SerializeField] bool canHeal;
    public int maxAmount, amountAvailable;

    /// <summary>
    /// grabs reference to the playerInfos script
    /// </summary>
    void Awake()
    {
        playerInfos = gameObject.GetComponent<PlayerInfos>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        moonwaterAmount.UpdateMoonWaterAmount();
    }

    void Update()
    {
        if(isHealing)
            playerController.speed = 0;
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
    /// if the player has more than 0 flasks available and less hp than their maxHP the available amount of healing flasks gets decreased by one
    /// afterwards the available amount of flasks is updated
    /// </summary>
    public void UseFlask()
    {
        if(amountAvailable > 0 && playerInfos.playerHealth < playerInfos.playerMaxHealth && canHeal)
        {
            isHealing = true;
            canHeal = false;
            amountAvailable -= 1;
            moonwaterAmount.UpdateMoonWaterAmount();

            playerController.anim.SetBool("isHealing", isHealing);
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

    /// <summary>
    /// the player is healed by an 8th of a specific amount 8 times during the animation
    /// </summary>
    public void PlayerHealing()
    {
        float amountToHeal = (playerInfos.playerMaxHealth * healAmount) / 8;
        amountToHeal = Mathf.RoundToInt(amountToHeal);
        playerInfos.playerHealth += amountToHeal;
        if (playerInfos.playerHealth > playerInfos.playerMaxHealth)
        {
            playerInfos.playerHealth = playerInfos.playerMaxHealth;
        }
    }
    public void FinishedHealing()
    {
        isHealing = false;
        canHeal = true;
        playerController.speed = playerController.maxSpeed; 
        playerController.anim.SetBool("isHealing", isHealing);
    }
}
