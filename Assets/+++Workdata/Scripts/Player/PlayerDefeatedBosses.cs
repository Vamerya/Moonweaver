using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDefeatedBosses : MonoBehaviour
{
    [SerializeField] List<string> bossID;
    [SerializeField] List<bool> bossDefeated;


    /// <summary>
    /// Grabs ID off a boss the player collides with and puts it into a list to keep track
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss"))
        {
            var bossInfos = collision.GetComponentInParent<BossInfos>();
            if(!bossID.Contains(bossInfos.id))
            {
                bossID.Add(bossInfos.id);
            }
            
            if(bossInfos.bossHealth < 1)
            {
                bossDefeated.Add(true);
            }
        }
    }
}
