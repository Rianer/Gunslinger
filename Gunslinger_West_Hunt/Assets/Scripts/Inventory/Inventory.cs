using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //TODO add inventory limit validation: available slots/total slots
    private static Inventory inventory;
    private Dictionary<string, Item> lootableItems = new Dictionary<string, Item>();
    private Dictionary<ItemDetailSO, int> inventoryItems = new Dictionary<ItemDetailSO, int>();

    #region Callback Functions
    public delegate void OnLootableItemChanged();
    public OnLootableItemChanged onLootableItemChanged;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;
    #endregion Callback Functions

    private void Awake()
    {
        inventory = this;
    }

    private void Start()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke();
        }
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

            if(onLootableItemChanged != null)
            {
                onLootableItemChanged.Invoke();
            }
        }
    }

    public void RemoveItemFromLootableItems(Item targetItem)
    {
        if (lootableItems.ContainsKey(targetItem.Id))
        {
            lootableItems.Remove(targetItem.Id);
            DebugLootableItems();

            if (onLootableItemChanged != null)
            {
                onLootableItemChanged.Invoke();
            }
        }
    }

    public Dictionary<ItemDetailSO, int> GetInventoryItems()
    {
        return inventoryItems;
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

        if(onInventoryChanged != null)
        {
            onInventoryChanged.Invoke();
        }
    }

    public void RemoveItemFromInventory(ItemDetailSO itemDetails)
    {
        if (inventoryItems.ContainsKey(itemDetails))
        {
            if (inventoryItems[itemDetails] > 1)
            {
                inventoryItems[itemDetails] -= 1;
            }
            else
            {
                inventoryItems.Remove(itemDetails);
            }

            if (onInventoryChanged != null)
            {
                onInventoryChanged.Invoke();
            }
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

    public void CollectItem(Item item)
    {
        if (item != null)
        {
            AddItemToInventory(item.details);
            lootableItems.Remove(item.Id);
            Destroy(item.gameObject);

            if (onLootableItemChanged != null)
            {
                onLootableItemChanged.Invoke();
            }
        }
    }

    private void CollectAllItems()
    {
        List<Item> itemsToCollect = GetLootableItemsList();
        foreach (Item item in itemsToCollect)
        {
            CollectItem(item);
            AddItemToInventory(item.details);
        }

    }
}
