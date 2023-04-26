using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Details", menuName = "Item/New Item Details")]
public class ItemDetailSO : ScriptableObject
{
    public bool useSameSprite;
    public Sprite inGameSprite;
    public Sprite inventorySprite;
    public float weight;
    public float value;
    public string itemName;
    public ItemType type;
    public int armor;
    public int healthPoints;


    private void OnValidate()
    {
        if (useSameSprite)
        {
            inventorySprite = inGameSprite;
        }
        else if (inventorySprite == inGameSprite && !useSameSprite)
        {
            inventorySprite = null;
        }

        if (type != ItemType.equipment)
        {
            armor = 0;
        }
        if (type != ItemType.consumable)
        {
            healthPoints = 0;
        }
    }
}
