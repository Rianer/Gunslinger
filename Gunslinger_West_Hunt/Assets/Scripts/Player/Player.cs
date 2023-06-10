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
[RequireComponent(typeof(WeaponHandler))]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerCharactersiticsSO playerCharactersitics;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    [SerializeField] private GameObject weaponAnchor;
    private WeaponHandler weaponHandler;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        weaponHandler = GetComponent<WeaponHandler>();
        ApplyPlayerLoadout();
        weaponHandler.EquipWeapon();
    }

    private void ApplyPlayerLoadout()
    {
        GameObject weapon = Instantiate(GameManager.Instance.playerLoadout.gunPrefab, weaponAnchor.transform);
        weapon.transform.parent = weaponAnchor.transform;

    }
}
