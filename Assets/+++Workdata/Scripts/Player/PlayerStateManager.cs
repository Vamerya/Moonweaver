using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] AkState akState;
    bool inCombat;
    bool inBossFight;
    bool isHeathy;

    void Awake()
    {
        playerInfos = gameObject.GetComponentInParent<PlayerInfos>();
        akState = gameObject.GetComponent<AkState>();
    }

    [ContextMenu("SetState")]
    void SetPlayerState()
    {
        
    }
}
