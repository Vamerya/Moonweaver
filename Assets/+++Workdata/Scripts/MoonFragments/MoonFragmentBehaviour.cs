using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFragmentBehaviour : MonoBehaviour
{
    [SerializeField] PlayerInRangeCheck playerInRange;
    [SerializeField] GameObject companion;

    void Awake()
    {
        playerInRange = gameObject.GetComponent<PlayerInRangeCheck>();
    }

    /// <summary>
    /// checks if the player is in range
    /// If the Player is interacting the players gets +1 MoonFragment, a companion fragment is spawned and the player gets set as its parent object
    /// Sets the players isInteracting back to false
    /// Adds the instantiated companion to the list inside the playerInfos
    /// Destroys the gameobject
    /// </summary>
    /// <param name="collision">Whatever is colliding with this</param>
    void OnTriggerStay2D(Collider2D collision)
    {
        if(playerInRange.playerInRange)
        {
            if(collision.GetComponentInParent<PlayerController>().isInteracting)
            {
                collision.GetComponentInParent<PlayerInfos>().collectedMoonFragments += 1;
                GameObject newCompanion = Instantiate(companion, collision.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2)), Quaternion.identity, companion.GetComponent<CompanionBehaviour>().target = collision.transform);
                newCompanion.GetComponent<CompanionBehaviour>().target = collision.transform;
                SetParent(collision.transform, newCompanion);
                collision.GetComponentInParent<PlayerController>().isInteracting = false;
                collision.GetComponentInParent<PlayerInfos>().companions.Add(newCompanion);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Sets new parent to a child object
    /// </summary>
    /// <param name="_newParent">The new parent</param>
    /// <param name="_child">The child object that's bein assigned to the new parent</param>
    void SetParent(Transform _newParent, GameObject _child)
    {
        _child.transform.SetParent(_newParent);
    }
}
