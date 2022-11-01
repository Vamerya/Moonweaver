using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthflaskBehaviour : MonoBehaviour, IDataPersistence
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] DisplayMoonwaterAmount moonwaterAmount;
    [SerializeField] float healAmount;
    public int maxAmount, amountAvailable;

    void Awake()
    {
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }
    void Start()
    {
        moonwaterAmount.UpdateMoonWaterAmount();
    }

    public void LoadData(GameData data)
    {
        this.amountAvailable = data.moonwater;
    }

    public void SaveData(ref GameData data)
    {
        data.moonwater = this.amountAvailable;
    }

    public void UseFlask()
    {
        if(amountAvailable > 0 && playerInfos.playerHealth < playerInfos.playerMaxHealth)
        {
            amountAvailable -= 1;
            float amountToHeal = playerInfos.playerMaxHealth * healAmount;
            amountToHeal = Mathf.RoundToInt(amountToHeal);
            playerInfos.playerHealth += amountToHeal;
            moonwaterAmount.UpdateMoonWaterAmount();
        }
    }

    public void RefillFlask()
    {
        amountAvailable = maxAmount;
        moonwaterAmount.UpdateMoonWaterAmount();
    }

}
