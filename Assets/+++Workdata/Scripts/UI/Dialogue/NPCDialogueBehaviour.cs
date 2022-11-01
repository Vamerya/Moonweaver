using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueBehaviour : MonoBehaviour
{   
    [SerializeField] PlayerController playerController;
    [SerializeField] DialogueManager dialogueManager;

    bool playerInRange;
    public Sprite npcPotrait; 
    public string npcName, npcTitle;
    public bool reachedEndOfDialogue;

    [Multiline(10)]
    public string[] npcDialogue;



    void Start() 
    {
        reachedEndOfDialogue = false;
    }

    void Update() 
    {
        if (playerInRange && playerController.isInteracting)
        {
            dialogueManager.ShowDialogueBox(npcPotrait, npcName, npcTitle, npcDialogue);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerController.isInteracting = false;
        }
    }
}
