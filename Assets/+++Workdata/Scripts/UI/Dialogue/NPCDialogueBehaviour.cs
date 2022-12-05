using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueBehaviour : MonoBehaviour
{   
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject indicator;
    bool playerInRange;
    


    void Start() 
    {
    
    }

    void Update() 
    {
        if (playerInRange && playerController.isInteracting)
        {
            //SendMessage("OnUse", this.transform, SendMessageOptions.DontRequireReceiver);
            //StartCoroutine(NegateInteraction());
        }
    }

    IEnumerator NegateInteraction()
    {
        yield return new WaitForSecondsRealtime(.4f);
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
            indicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;
            indicator.SetActive(false);
            playerController.isInteracting = false;
        }
    }
}
