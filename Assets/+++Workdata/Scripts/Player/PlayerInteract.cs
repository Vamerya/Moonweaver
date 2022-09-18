using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject[] interactableType;
    InteractableType interactableTypeID;
    PlayerController playerController;
    PlayerInfos playerInfos;

    //INDEX: 0 = RangedWeapon, 1 = ALtar, 2 = Dialogue Trigger 

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
        interactableType = GameObject.FindGameObjectsWithTag("Interactable");
    }

    void Start()
    {
        interactableTypeID = gameObject.GetComponent<InteractableType>();
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Interactable") && playerController.isInteracting) 
        {
            if(interactableTypeID.interactableID == 0) //COLLECT RANGED WEAPON
            {
                playerInfos.obtainedRangedWeapon = true;
                Destroy(collision.gameObject);
            }  

            if(interactableTypeID.interactableID == 1) //OPENS LEVEL UP UI
            {
                Debug.Log("LevelUp UI");
                //OPEN LEVELUP UI  
            }   

            if(interactableTypeID.interactableID == 2) //OPENS DIALOGUE UI
            {
                Debug.Log("Dialogue");
                //Triggers dialogue
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interactableType = GameObject.FindGameObjectsWithTag("Interactable");
    }
}
