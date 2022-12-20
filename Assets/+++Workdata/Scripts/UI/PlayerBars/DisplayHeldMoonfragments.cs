using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHeldMoonfragments : MonoBehaviour
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] TextMeshProUGUI collectedMoonFragments;

    /// <summary>
    /// Displays how many Moonfragments the player has currentl collected
    /// </summary>
    void Update()
    {
        collectedMoonFragments.text = playerInfos.collectedMoonFragments.ToString() + " Moon Fragments";
    }
}
