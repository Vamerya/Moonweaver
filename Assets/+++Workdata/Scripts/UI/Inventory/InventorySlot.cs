using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public ItemBehaviour itemBehaviour;
    public Image itemIcon;
    public bool isOccupied;
    public int itemCounter;
    public TextMeshProUGUI itemCounterText;


    void Awake()
    {
        itemBehaviour = gameObject.GetComponent<ItemBehaviour>();
    }

    void Start()
    {
        if(!isOccupied)
        {
            itemIcon.enabled = false;
            //itemCounterText.text = "";
        }
    }

    public void AddNewItem(ItemBehaviour newItemBehaviour)
    {
        itemBehaviour = newItemBehaviour;

        itemCounter += newItemBehaviour.itemValue;
        itemCounterText.text = itemCounter.ToString();
        isOccupied = true;
        itemIcon .enabled = true;
        itemIcon.sprite = itemBehaviour.itemSO.itemSprite;
    }

    public void AddItemValue(int itemValue)
    {
        itemCounter += itemValue;
        //itemCounterText.text = itemCounter.ToString();   
    }
    void RemoveItem()
    {
        isOccupied = false;
    }

}
