using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#region REQUIRE_COMPONENTS
[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerCharactersiticsSO playerCharactersitics;
    [HideInInspector] public Health health;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(PlayerCharactersiticsSO playerCharactersitics)
    {
        this.playerCharactersitics = playerCharactersitics;
        SetHealth();
    }


    private void SetHealth()
    {
        health.SetStartingHealth(playerCharactersitics.playerHealthAmount);

    }
}
