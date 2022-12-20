using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedMoonlightBehaviour : MonoBehaviour
{
    public float moonlightAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetComponentInParent<PlayerInfos>().isAlive)
        {
            collision.GetComponentInParent<PlayerLevelBehaviour>().moonLight += moonlightAmount;
            Destroy(gameObject);
        }
    }
}
