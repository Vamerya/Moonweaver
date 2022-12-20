using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    public float spikeDamage;

    public float DetermineSpikeDamage(float dmg)
    {
        spikeDamage = dmg;
        return spikeDamage;
    }

    public float SpikeDamage()
    {
        return spikeDamage;
    }
    void DestroySpike()
    {
        Destroy(gameObject);
    }
}
