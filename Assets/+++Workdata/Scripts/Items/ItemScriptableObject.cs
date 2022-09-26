using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string ItemName;
    public Sprite itemSprite;
    int currentStackValue;
    public int maxStackValue, itemID;
}