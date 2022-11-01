using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;


    void Start()
    {
        
    }

    public void DialogueNextPage()
    {
        if(dialogueManager.counter != dialogueManager.dialogueLength - 2)
            dialogueManager.counter++;
        else
            dialogueManager.dialogueEnded = true;
    }

    public void CloseDialogue()
    {
        dialogueManager.HideDialogueBox();
        dialogueManager.hasExited = true;
    }
}
