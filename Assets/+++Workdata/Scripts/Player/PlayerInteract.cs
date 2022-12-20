using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls interactions for the player
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    #region Variables
    [Header ("Main Components")]
    PlayerController playerController;
    PlayerInfos playerInfos;
    [SerializeField] LevelUpManager levelUpManager;
   
    #endregion

    /// <summary>
    /// grabs necessary references to other scripts
    /// </summary>
    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerInfos = gameObject.GetComponent<PlayerInfos>();
    }

    void Start()
    {
 
    }

    void Update()
    {
        
    }

    /// <summary>
    /// this is used to call specific functions when the player is in range of the interactable and presses the according key
    /// </summary>
    /// <param name="collision">compareTag is used to determine what the player is interacting with</param>
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponPickup") && playerController.isInteracting) //COLLECT RANGED WEAPON
        {
            playerInfos.obtainedRangedWeapon = true;
            Destroy(collision.gameObject);
        }
    }
}
