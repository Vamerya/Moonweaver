using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class NPCDialogueBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject indicator;
    [SerializeField] DialogueSystemTrigger dialogueTrigger;
    bool playerInRange;


    void Awake()
    {
        dialogueTrigger = gameObject.GetComponent<DialogueSystemTrigger>();
    }

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
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (indicator)
                indicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (indicator)
                indicator.SetActive(false);
            playerController.isInteracting = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (playerController.isInteracting)
        {
            dialogueTrigger.enabled = true;
            Destroy(indicator);
        }
    }
}
