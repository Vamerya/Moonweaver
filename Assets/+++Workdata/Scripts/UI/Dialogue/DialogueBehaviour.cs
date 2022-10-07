using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviour : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] GameObject _canvas;
    [SerializeField] PlayerController playerController;
    bool playerInRange;

    void Awake()
    {
        canvas = gameObject.GetComponentInChildren<Canvas>();
    }
    void Update()
    {
        if(playerInRange && playerController.isInteracting)
            _canvas.SetActive(true);
        else
            _canvas.SetActive(false);
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
