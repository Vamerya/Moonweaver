using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Choose Dialogue Style")]
    //if true, interacting a second time shows a summary, if false the last page (Total Length - 1) of the dialogue is shown
    //the very last slot of the dialogue array is used for the summary, the page before that is supposed to be the last part
    //of the dialogue
    public bool endDialogueSummary;

    [Header("Variables")]
    public GameObject _dialogueboxContainer;
    public GameObject _inventoryCanvas;
    public GameObject _hotbarCanvas;
    public GameObject _backgroundDim;
    public GameObject _nextButton, _closeButton;
    public Image potraitImage;
    public TextMeshProUGUI dialogueName, dialogueDescription, dialogueText;
    public PlayerController playerController;
    public int counter, dialogueLength;
    public string titleColor; //<color=#FFFFFF>
    public bool dialogueEnded, hasExited;
    public float timeScale;



    void Start()
    {
        dialogueEnded = false;
    }

    void Update()
    {
        if(dialogueEnded)
        {
            _nextButton.SetActive(false);
            _closeButton.SetActive(true);
        }
    }

    public void ShowDialogueBox(Sprite npcPotrait, string npcName, string npcDescription, string[] npcDialogue)
    {
        Time.timeScale = timeScale;

        _dialogueboxContainer.SetActive(true);
        _backgroundDim.SetActive(true);
        _inventoryCanvas.SetActive(false);
        _hotbarCanvas.SetActive(false);
        _nextButton.SetActive(true);
        dialogueLength = npcDialogue.Length;
        potraitImage.sprite = npcPotrait;
        dialogueName.text = npcName;
        dialogueDescription.text = titleColor + npcDescription;
        dialogueText.text = npcDialogue[counter];

        playerController.isTalking = true;

        if(dialogueEnded && hasExited && endDialogueSummary)
        {
            Time.timeScale = timeScale;

            _dialogueboxContainer.SetActive(true);
            _backgroundDim.SetActive(true);
            _inventoryCanvas.SetActive(false);
            _hotbarCanvas.SetActive(false);
            _nextButton.SetActive(true);
            dialogueLength = npcDialogue.Length;
            potraitImage.sprite = npcPotrait;
            dialogueName.text = npcName;
            dialogueDescription.text = titleColor + npcDescription;
            dialogueText.text = npcDialogue[npcDialogue.Length - 1];

            playerController.isTalking = true;
        }
        else if(dialogueEnded && hasExited && !endDialogueSummary)
        {
            Time.timeScale = timeScale;

            _dialogueboxContainer.SetActive(true);
            _backgroundDim.SetActive(true);
            _inventoryCanvas.SetActive(false);
            _hotbarCanvas.SetActive(false);
            _nextButton.SetActive(true);
            dialogueLength = npcDialogue.Length;
            potraitImage.sprite = npcPotrait;
            dialogueName.text = npcName;
            dialogueDescription.text = titleColor + npcDescription;
            dialogueText.text = npcDialogue[npcDialogue.Length - 2];

            playerController.isTalking = true;
        }
    }

    public void HideDialogueBox()
    {
        Time.timeScale = 1;

        _dialogueboxContainer.SetActive(false);
        _backgroundDim.SetActive(false);
        _hotbarCanvas.SetActive(true);

        playerController.isTalking = false;
        playerController.isInteracting = false;
    }
}
