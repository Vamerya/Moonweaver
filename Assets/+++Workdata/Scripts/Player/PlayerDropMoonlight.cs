using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropMoonlight : MonoBehaviour
{
    [SerializeField] PlayerLevelBehaviour playerLevelBehaviour;
    [SerializeField] GameObject droppedMoonlight;
    [SerializeField] int amountToDrop;


    GameObject moonlight;
    void Awake()
    {
        playerLevelBehaviour = gameObject.GetComponent<PlayerLevelBehaviour>();
    }

    public void DropMoonlight(float val)
    {
        Debug.LogWarning("Moonlight Amount " + val);
        if(!moonlight)
        {
            InstantiateMoonLight(val);
        }
        else
        {
            Destroy(moonlight);
            InstantiateMoonLight(val);
        }
    }

    void InstantiateMoonLight(float _moonLightAmount)
    {
        moonlight = Instantiate(droppedMoonlight, this.transform.position, Quaternion.identity);
        moonlight.GetComponent<DroppedMoonlightBehaviour>().moonlightAmount = _moonLightAmount;
        playerLevelBehaviour.moonLight = 0;
    }
}
