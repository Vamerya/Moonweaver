using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatSlot : MonoBehaviour
{
    
    [SerializeField] int slotID;
    public Image statIcon;
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statValue;
    public PlayerMeleeWeaponBehaviour playerMeleeWeapon;
    public PlayerStatsBehaviour playerStats;


    void Awake()
    {
        // statName = gameObject.GetComponent<TextMeshProUGUI>();
        // statValue = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        UpdateStatValues();
    }

    void UpdateStatValues()
    {
        statName.text = playerStats.DetermineDisplayedStatName(slotID);
        statValue.text = playerStats.DetermineDisplayedStatValue(slotID);
    }
}
