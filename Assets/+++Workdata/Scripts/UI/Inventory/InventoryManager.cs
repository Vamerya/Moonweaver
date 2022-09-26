using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlot; //0-14 = Inventory Slots, 15-19 = Hotbar Slots

    void Awake()
    {

    }

    public bool AddItemToInventory(ItemBehaviour newItemBehaviour)
    {
        for(int i = 0; i < inventorySlot.Length; i++)
        {
            if(!inventorySlot[i].isOccupied)
            {
                inventorySlot[i].AddNewItem(newItemBehaviour);
                return true;
            }
            else
            {
                if(newItemBehaviour.itemSO.itemID == inventorySlot[i].itemBehaviour.itemSO.itemID)
                {
                    inventorySlot[i].AddItemValue(newItemBehaviour.itemValue);
                    return true;
                }
            }
        }
        return false;
    } 
}
