using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(BoxCollider2D))]
public class ItemLooter : MonoBehaviour
{
    private CollisionHandler collisionHandler;
    private Inventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<Inventory>();
        collisionHandler = new CollisionHandler(playerInventory);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionHandler.HandleLootableEntry(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionHandler.HandleLootableExit(collision.gameObject);
    }
}
