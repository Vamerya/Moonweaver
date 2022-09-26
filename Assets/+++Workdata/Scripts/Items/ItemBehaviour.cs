using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemScriptableObject itemSO;
    public int itemValue;


    void Awake()
    {
    
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Item ID: " + itemSO.itemID);
            if(inventoryManager.AddItemToInventory(this))
            {
                Destroy(gameObject);
            }
            Debug.Log("No free space in inventory");
        }
    }
}
