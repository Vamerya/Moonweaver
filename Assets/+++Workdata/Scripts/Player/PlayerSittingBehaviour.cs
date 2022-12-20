using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls when and how and where the player is sitting and grabs references from nearby benches that are necessary for all the functions
/// </summary>
public class PlayerSittingBehaviour : MonoBehaviour
{
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    Camera mainCam;
    public BenchBehaviour benchBehaviour;
    public GameObject cameraLookAtPoint;
    public float focalLength;
    public bool canSit;
    [SerializeField] Vector3 offset;

    /// <summary>
    /// Grabs references to necessary components
    /// </summary>
    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    /// <summary>
    /// Sets bools based on given values
    /// </summary>
    void Update()
    {
        if(canSit && playerController.isInteracting)
        {
            playerController.isSitting = true;
            spriteRenderer.sortingOrder = 2;
        }
        else if(!playerController.isInteracting)
        {
            playerController.isSitting = false;
            spriteRenderer.sortingOrder = 1;
        }

        playerController.anim.SetBool("isSitting", playerController.isSitting);
    }

    /// <summary>
    /// Checks if the player collided with an object tagged as "bench" and sets the canSit bool to true
    /// </summary>
    /// <param name="collision">This is what the player collided with</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bench"))
        {
            canSit = true;
            cameraLookAtPoint = collision.GetComponent<BenchBehaviour>().cameraLookAtPoint;
            focalLength = collision.GetComponent<BenchBehaviour>().focalLength;
            benchBehaviour = collision.GetComponent<BenchBehaviour>();
        }
    }

    /// <summary>
    /// Checks if the player collided with an object tagged as "bench" and sets the canSit bool to false
    /// </summary>
    /// <param name="collision">This is what the player collided with</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Bench"))
        {
            canSit = false;
        }
    }

    /// <summary>
    /// If the player is sitting their position gets set to the position of the object they collided with (the bench) + an offset to ensure the player
    /// is placed on the bench properly
    /// </summary>
    /// <param name="collision">This is what the player collided with</param>
    void OnTriggerStay2D(Collider2D collision)
    {
        if(playerController.isSitting)
        {
            this.transform.position = collision.transform.position + offset;
        }
    }
}
