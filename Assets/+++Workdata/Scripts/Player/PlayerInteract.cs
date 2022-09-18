using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerController playerController;
    PlayerInfos playerInfos;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    void Start()
    {
 
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.CompareTag("WeaponPickup") && playerController.isInteracting) //COLLECT RANGED WEAPON
        {
            playerInfos.obtainedRangedWeapon = true;
            Destroy(collision.gameObject);
        }  

        if(collision.CompareTag("Shrine") && playerController.isInteracting) //OPENS LEVEL UP UI
        {
            Debug.Log("LevelUp UI");
            //OPEN LEVELUP UI  
        }   

        if(collision.CompareTag("Dialogue") && playerController.isInteracting) //OPENS DIALOGUE UI
        {
            Debug.Log("Dialogue");
            //Triggers dialogue
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
