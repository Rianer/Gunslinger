using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BulletExtended : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bulletCollider;
    [SerializeField] private ParticleSystem bulletHitSurfaceParticles;
    private int damage;
    private DamageType damageType;
    private float bulletSpeed;
    private BulletType bulletType;
    private BulletDamageArea damageArea;
    public LayerMask wallsLayer;
    public LayerMask charactersLayer;
    public int wallsPiercingCount;
    public int charactersPiercingCount;
    private int remainingWallPiercing;
    private int remainingCharactersPiercing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<BoxCollider2D>();
    }

    public void ApplyProperties(WeaponStatsSO weaponStats)
    {
        damage = weaponStats.damage;
        damageType = weaponStats.damageType;
        damageArea = weaponStats.damageArea;
        bulletSpeed = weaponStats.bulletSpeed;
        bulletType = weaponStats.bulletType;
    }

    public void Fire(Vector3 direction)
    {
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(bulletHitSurfaceParticles, rb.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.IsInLayerMask(wallsLayer))
        {
            if (wallsPiercingCount > 0)
            {
                wallsPiercingCount--;
                UpdateCollision(true, collider);
            }
            else
            {
                UpdateCollision(false, collider);
            }
            OnWallHit(collider);
        }
        if (collider.gameObject.IsInLayerMask(charactersLayer))
        {
            if (charactersPiercingCount > 0)
            {
                charactersPiercingCount--;
                UpdateCollision(true, collider);
            }
            else
            {
                UpdateCollision(false, collider);
            }
            OnCharacterHit(collider);
        }
    }

    private void UpdateCollision(bool ignoreCollision, Collider2D collider)
    {
        Physics2D.IgnoreCollision(bulletCollider, collider, ignoreCollision);
    }

    private void OnCharacterHit(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
        {
            VitalityManager vitalityManager = collider.gameObject.GetComponent<VitalityManager>();
            if (vitalityManager != null)
                DealDamage(vitalityManager);
        }
    }

    private void OnWallHit(Collider2D collider)
    {
        Debug.Log($"Wall Hit");
    }

    private void DealDamage(VitalityManager targetVitalityManager)
    {
        //TODO: calculate complex damage model
        targetVitalityManager.ReceiveDamage(damage);

    }
}
