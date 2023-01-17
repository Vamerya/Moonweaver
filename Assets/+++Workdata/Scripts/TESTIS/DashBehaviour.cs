using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : MonoBehaviour
{
    public Transform player, boss;
    public Transform dashSpot;

    public float radius;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - boss.position;
        dashSpot.position = boss.position;

        dashSpot.position = dashSpot.position - dir;

        float distance = Vector3.Distance(dashSpot.position, boss.position);
        Vector3 fromOriginToObject = dashSpot.position - boss.position;
        fromOriginToObject *= radius / distance;
        dashSpot.position = boss.position + fromOriginToObject;
        /*
        // If the new distance is larger than the radius
        if (distance > radius) 
        {
            Vector3 fromOriginToObject = c.position - b.position;
            fromOriginToObject *= radius / distance;
            c.position = b.position + fromOriginToObject;
        }
        */
        /*
        // If the new distance is smaller than the radius
        if (distance < radius) 
        {
            Vector3 fromOriginToObject = c.position - b.position;
            fromOriginToObject *= radius / distance; 
            c.position = b.position + fromOriginToObject;
        }    
        */
    }
}
