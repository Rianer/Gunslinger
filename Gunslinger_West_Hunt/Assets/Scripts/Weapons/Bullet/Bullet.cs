using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bulletCollider;
    [SerializeField] private ParticleSystem bulletHitSurfaceParticles;
    private int damage;
    private DamageType damageType;
    private float bulletSpeed;
    private BulletType bulletType;
    private BulletDamageArea damageArea;
    public int semiObstacleLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, semiObstacleLayer);
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);

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
        OnCharacterHit(collision.collider);
        Debug.Log(collision.gameObject.layer == 13);
        Destroy(gameObject);   
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

    private void DealDamage(VitalityManager targetVitalityManager)
    {
        //TODO: calculate complex damage model
        targetVitalityManager.ReceiveDamage(damage);

    }
}
