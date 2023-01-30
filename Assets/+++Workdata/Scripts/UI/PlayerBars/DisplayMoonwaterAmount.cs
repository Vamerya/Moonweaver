using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayMoonwaterAmount : MonoBehaviour
{
    [SerializeField] PlayerHealthflaskBehaviour flaskAmount;
    [SerializeField] Image flaskOne;
    [SerializeField] Image flaskTwo;
    [SerializeField] Image flaskThree;
    [SerializeField] float percentageAmount;

    public void UpdateMoonWaterAmount()
    {
        switch (flaskAmount.amountAvailable)
        {
            case 0:
                flaskOne.enabled = false;
                flaskTwo.enabled = false;
                flaskThree.enabled = false;
                break;
            case 1:
                flaskOne.enabled = true;
                flaskTwo.enabled = false;
                flaskThree.enabled = false;
                break;
            case 2:
                flaskOne.enabled = true;
                flaskTwo.enabled = true;
                flaskThree.enabled = false;
                break;
            case 3:
                flaskOne.enabled = true;
                flaskTwo.enabled = true;
                flaskThree.enabled = true;
                break;
            default:
                break;
        }
    }

}
