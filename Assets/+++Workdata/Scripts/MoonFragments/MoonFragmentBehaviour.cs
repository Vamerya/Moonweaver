using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFragmentBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerInfos>().collectedMoonFragments += 1;
            Destroy(gameObject);
        }
    }
}
