using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryPanelManager : MonoBehaviour
{
    private static InventoryPanelManager inventoryPanelManager;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private void Awake()
    {
        inventoryPanelManager = this;
    }

    static public InventoryPanelManager Instance()
    {
        return inventoryPanelManager;
    }

    public void ApplyItemDescription(string details)
    {
        itemDescription.text = details;
    }
}
