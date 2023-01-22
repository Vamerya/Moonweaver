using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthstateManager : MonoBehaviour
{
    [SerializeField] PlayerInfos playerInfos;


    void Awake()
    {
        playerInfos = GetComponentInParent<PlayerInfos>();
    }

    void Update()
    {
        if(playerInfos.playerHealthPercentage < .3)
        {
            AkSoundEngine.SetState("PlayerHealthState", "LowLife");
        }
        else
            AkSoundEngine.SetState("PlayerHealthState", "Healthy");
    }
}
