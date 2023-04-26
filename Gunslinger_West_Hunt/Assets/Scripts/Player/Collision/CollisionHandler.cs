using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler
{
    private Inventory playerInventory;
    private Item currentItem;

    public CollisionHandler(Inventory inventory)
    {
        playerInventory = inventory;
    }

    public bool CheckLootable(GameObject targetObject)
    {
        Item item = targetObject.GetComponent<Item>();
        if (item != null)
        {
            currentItem = item;
            return true;
        }
        return false;
    }

    public void HandleLootableEntry(GameObject targetObject)
    {
        if (CheckLootable(targetObject))
        {
            playerInventory.AddItemToLootableItems(currentItem);
        }
    }

    public void HandleLootableExit(GameObject targetObject)
    {
        if (CheckLootable(targetObject))
        {
            playerInventory.RemoveItemFromLootableItems(currentItem);
        }
    }

    
}
