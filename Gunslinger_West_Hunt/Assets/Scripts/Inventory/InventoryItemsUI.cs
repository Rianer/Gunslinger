using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InventoryItemsUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParent;
    private List<InventorySlot> slots;
    private Inventory inventory;


    void Start()
    {
        inventory = Inventory.GetInstance();
        inventory.onInventoryChanged += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList<InventorySlot>();
    }


    void UpdateUI()
    {
        Dictionary<ItemDetailSO, int> inventoryItems = inventory.GetInventoryItems();
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventoryItems.Count)
            {
                ItemDetailSO key = inventoryItems.Keys.ToList<ItemDetailSO>()[i];
                slots[i].UpdateItemDetails(key, inventoryItems[key]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
