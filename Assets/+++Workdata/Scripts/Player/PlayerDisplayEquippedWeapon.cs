using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplayEquippedWeapon : MonoBehaviour
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] GameObject weaponOne;
    [SerializeField] GameObject weaponTwo;

    void Start()
    {
        SwapDisplayedWeapon();
    }
    
    public void SwapDisplayedWeapon()
    {
        switch (playerInfos.inventoryState)
        {
            case 0:
                weaponOne.SetActive(true);
                weaponTwo.SetActive(false);
                break;
            case 1:
                weaponOne.SetActive(false);
                weaponTwo.SetActive(true);
                break;
            default:
                break;
        }
    }
}
