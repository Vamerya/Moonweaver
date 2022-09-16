using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerController playerController;
    PlayerInfos playerInfos;
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    void Update()
    {

    }
    public void Attack()
    {
        
    }
    
    public void AttackRelease()
    {
        
    }
}
