using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] LevelUpManager levelUpManager;
    [SerializeField] GameObject levelUpUI;
    bool playerInRange;

    void Awake()
    {
        
    }
    void Update()
    {
        if(playerController.isInteracting && playerInRange)
        {
            if(levelUpUI.activeInHierarchy)
                levelUpManager.ToggleLevelUpUI();

            else
                levelUpManager.ToggleLevelUpUI();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInRange = false;
    }
}
