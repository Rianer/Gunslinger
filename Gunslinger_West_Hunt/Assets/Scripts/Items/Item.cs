using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] private string id;
    public ItemDetailSO details;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private string itemName;
    [SerializeField] private ItemType itemType;
    [SerializeField] private float weight;
    [SerializeField] private float value;

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

    public Item()
    {
        id = Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        id = Guid.NewGuid().ToString();
        ApplyDetails();
    }

    protected virtual void ApplyDetails()
    {
        spriteRenderer.sprite = details.inGameSprite;
        weight = details.weight;
        value = details.value;
        itemName = details.itemName;
        itemType = details.type;
    }

}
