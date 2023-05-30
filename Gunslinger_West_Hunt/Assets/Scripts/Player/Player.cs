using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#region REQUIRE_COMPONENTS
[DisallowMultipleComponent]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerCharactersiticsSO playerCharactersitics;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(PlayerCharactersiticsSO playerCharactersitics)
    {
        this.playerCharactersitics = playerCharactersitics;
    }
}
