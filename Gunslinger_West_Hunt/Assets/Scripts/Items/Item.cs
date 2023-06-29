using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent (typeof(Transform))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] public ItemDetailSO details;
    private SpriteRenderer spriteRenderer;
    private string itemName;
    private ItemType itemType;
    private float weight;
    private float value;

    public string Id
    {
        get { return id; }
    }
    public string ItemName
    {
        get { return itemName; }
    }
    public ItemType ItemType
    {
        get { return itemType; }
    }
    public float Weight
    {
        get { return weight; }
    }
    public float Value
    {
        get { return value; }
    }

    //public Item()
    //{
    //    id = Guid.NewGuid().ToString();
    //}

    //public Item(ItemDetailSO details)
    //{
    //    id = Guid.NewGuid().ToString();
    //    this.details = details;
    //    ApplyDetails();
    //}

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        id = Guid.NewGuid().ToString();
        ApplyDetails();
    }

    protected virtual void ApplyDetails()
    {
        spriteRenderer.sprite = details.inGameSprite;
        spriteRenderer.sortingLayerName = "Items";
        GetComponent<BoxCollider2D>().isTrigger = true;
        weight = details.weight;
        value = details.value;
        itemName = details.itemName;
        itemType = details.type;
    }

    public virtual void HandleClick()
    {
        Debug.Log("Click not implemented");
    }

}
