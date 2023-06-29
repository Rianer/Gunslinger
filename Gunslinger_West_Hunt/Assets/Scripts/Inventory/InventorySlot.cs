using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    [SerializeField]private Image icon;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image removeButtonImage;
    private Item item;
    private ItemDetailSO itemDetails;
    private int quantity;

    private void Start()
    {
        ClearSlot();
    }

    public void UpdateItemDetails(ItemDetailSO newItemDetails, int quantity)
    {
        itemDetails = newItemDetails;
        icon.sprite = itemDetails.inventorySprite;
        icon.enabled = true;
        this.quantity = quantity;
        textMesh.enabled = true;
        textMesh.text = this.quantity.ToString();
        removeButton.interactable = true;
        removeButtonImage.color = new Color(255,255,255,1);
    }

    public void UpdateItem(Item newItem)
    {
        textMesh.enabled = false;
        item = newItem;
        icon.sprite = newItem.details.inventorySprite;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        textMesh.text = string.Empty;
        textMesh.enabled = false;
        quantity = 0;
        item = null;
        itemDetails = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        removeButtonImage.color = new Color(255, 255, 255, 0);
    }

    public void CollectItem()
    {
        Inventory.GetInstance().CollectItem(item);
    }


    public void DropItem()
    {
        HideItemDetails();
        Inventory.GetInstance().RemoveItemFromInventory(itemDetails);
    }

    public void UseItem()
    {
        if(itemDetails.type == ItemType.consumable)
        {
            itemDetails.UseItem();
            DropItem();
        }
    }

    public void ShowItemDetails()
    {
        if(itemDetails != null)
        {
            InventoryPanelManager.Instance().ApplyItemDescription(itemDetails.description);
        }
    }

    public void HideItemDetails()
    {
        if (itemDetails != null)
        {
            InventoryPanelManager.Instance().ApplyItemDescription("");
        }
    }
}
