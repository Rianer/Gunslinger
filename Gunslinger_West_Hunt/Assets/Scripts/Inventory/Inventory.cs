using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, Item> lootableItems = new Dictionary<string, Item>();
    private List<ItemDetailSO> itemList = new List<ItemDetailSO>();

    public List<Item> GetLootableItemsList()
    {
        return new List<Item>(lootableItems.Values);
    }

    public void DebugLootableItems()
    {
        String debugResult = "";
        foreach(KeyValuePair<string, Item> item in lootableItems)
        {
            debugResult += String.Format("Item {0}: {1}\n", item.Key, item.Value.ItemName);
        }
        Debug.Log(debugResult);
    }

    public void AddItemToLootableItems(Item newItem)
    {
        if (!lootableItems.ContainsKey(newItem.Id))
        {
            lootableItems.Add(newItem.Id, newItem);
            DebugLootableItems();
        }
    }

    public void RemoveItemFromLootableItems(Item targetItem)
    {
        if (lootableItems.ContainsKey(targetItem.Id))
        {
            lootableItems.Remove(targetItem.Id);
            DebugLootableItems();
        }
    }

    public void AddItemToInventory(Item item)
    {

    }

}
