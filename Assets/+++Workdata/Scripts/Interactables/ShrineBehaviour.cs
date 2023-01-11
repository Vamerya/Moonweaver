using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls when the player is in range of a shrine and refills Moonwater upon interacting
/// </summary>
public class ShrineBehaviour : MonoBehaviour
{
    [Header("Check this if this is the main Shrine")]
    [SerializeField] public bool mainShrine;

    [Header("Variables")]
    [SerializeField] Animator anim;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerHealthflaskBehaviour healthflaskBehaviour;
    [SerializeField] public ShrineManager shrineManager;
    [SerializeField] GameObject _indicator;
    [SerializeField] bool playerInRange;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    /// <summary>
    /// opens the leveUpUI if the player is in range and interacting
    /// closes it if the player is neither
    /// </summary>
    void Update()
    {
        if(playerInRange && playerController.isInteracting)
        {
            
        }
        else if(!playerController.isInteracting)
        {
            shrineManager.HideShrineMenu();
        }

        if(playerInRange)
            _indicator.SetActive(true);
        else
            _indicator.SetActive(false);
    }

    /// <summary>
    /// sets playerInRange to true upon colliding with the player
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
            anim.SetBool("playerInRange", playerInRange);
        }
        
    }

    /// <summary>
    /// sets playerInRange and isInteracting to false when the player leaves the hitbox
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerController.isInteracting = false;
            anim.SetBool("playerInRange", playerInRange);
        }
    }
}
