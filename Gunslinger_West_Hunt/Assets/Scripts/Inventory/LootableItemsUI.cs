using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootableItemsUI : MonoBehaviour
{
    [SerializeField]private Transform itemsParent;
    private List<InventorySlot> slots;
    private Inventory inventory;

    void Start()
    {
        inventory = Inventory.GetInstance();
        inventory.onLootableItemChanged += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList<InventorySlot>();
    }

    void UpdateUI()
    {
        List<Item> lootableItems = inventory.GetLootableItemsList();
        for(int i = 0; i < slots.Count; i++)
        {
            if(i < lootableItems.Count)
            {
                slots[i].UpdateItem(lootableItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
