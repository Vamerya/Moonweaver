using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthflaskBehaviour : MonoBehaviour
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
        amountAvailable = maxAmount;
        moonwaterAmount.UpdateMoonWaterAmount();
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
