using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    private bool axisInUse = false;
    void Start()
    {
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Inventory") == 1)
        {
            if (!axisInUse)
            {
                Debug.Log("Access inventory");
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                Cursor.visible = !Cursor.visible;
                axisInUse = true;
            }
        }
        else if (Input.GetAxisRaw("Inventory") == 0)
        {
            axisInUse = false;
        }
    }
}
