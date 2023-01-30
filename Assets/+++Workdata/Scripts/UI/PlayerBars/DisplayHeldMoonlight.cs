using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHeldMoonlight : MonoBehaviour
{
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] TextMeshProUGUI heldMoonLight;

    /// <summary>
    /// Displays how much Moonlight the player has currently available
    /// </summary>
    void Update()
    {
        heldMoonLight.text = playerLevelBehaviour.moonLight.ToString();
    }
}
