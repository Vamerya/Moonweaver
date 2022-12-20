using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDefeatedBosses : MonoBehaviour
{
    [SerializeField] List<string> bossID;
    [SerializeField] List<bool> bossDefeated;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss"))
        {
            var bossInfos = collision.GetComponent<BossInfos>();
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
