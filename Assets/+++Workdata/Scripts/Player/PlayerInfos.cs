using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    PlayerController playerController;
    PlayerCombat playerCombat;
    public float playerHealth, playerStamina;
    public int inventoryState;
    public bool obtainedRangedWeapon, swappedWeapon;



    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
    }

    void Update()
    {

    }

    public void SwapWeapon()
    {
        if(obtainedRangedWeapon)
            swappedWeapon = !swappedWeapon;

        if(swappedWeapon)
            inventoryState = 1;
        else
            inventoryState = 0;

        playerController.anim.SetFloat("InventoryState", inventoryState);
    }
}
