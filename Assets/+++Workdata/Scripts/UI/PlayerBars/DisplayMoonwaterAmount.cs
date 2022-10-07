using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayMoonwaterAmount : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI heldMoonwater;
    [SerializeField] PlayerHealthflaskBehaviour flaskAmount;

    public void UpdateMoonWaterAmount()
    {
        heldMoonwater.text = flaskAmount.amountAvailable.ToString();
    }
}
