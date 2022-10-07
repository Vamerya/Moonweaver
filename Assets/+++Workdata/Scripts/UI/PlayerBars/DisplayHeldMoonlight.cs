using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHeldMoonlight : MonoBehaviour
{
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] TextMeshProUGUI heldMoonLight;

    void Update()
    {
        heldMoonLight.text = playerLevelBehaviour.moonLight.ToString() + " Moonlight";
    }

}
