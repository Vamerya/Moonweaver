using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerHealthflaskBehaviour healthflaskBehaviour;
    [SerializeField] LevelUpManager levelUpManager;
    [SerializeField] GameObject levelUpUI;
    bool playerInRange;

    void Update()
    {
        if(playerInRange && playerController.isInteracting)
        {
            levelUpUI.SetActive(true);
            healthflaskBehaviour.RefillFlask();
        }
        else
            levelUpUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInRange = true;
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
