using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory inventory;
    private Dictionary<string, Item> lootableItems = new Dictionary<string, Item>();
    private Dictionary<ItemDetailSO, int> inventoryItems = new Dictionary<ItemDetailSO, int>();

    private void Awake()
    {
        inventory = this;
    }

    public static Inventory GetInstance()
    {
        return inventory;
    }

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

    public void AddItemToInventory(ItemDetailSO itemDetails)
    {
        if (inventoryItems.ContainsKey(itemDetails))
        {
            inventoryItems[itemDetails] += 1;
        }
        else
        {
            inventoryItems.Add(itemDetails, 1);
        }
    }

    public void DebugInventory()
    {
        string output = "";
        foreach(var item in inventoryItems)
        {
            output += item.Key.itemName + ": " + item.Value.ToString() + "\n";
        }
        Debug.Log(output);
    }

    private void ColectItem(Item item)
    {
        if (item != null)
        {
            lootableItems.Remove(item.Id);
            Destroy(item.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Item previousItem = null;
            foreach(Item item in lootableItems.Values)
            {
                ColectItem(previousItem);
                AddItemToInventory(item.details);
                previousItem = item;
            }
            ColectItem(previousItem);
        }
        if (Input.GetKeyDown("p"))
        {
            DebugInventory();
        }
    }
}
