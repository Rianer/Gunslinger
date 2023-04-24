using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private int damage;
    private DamageType damageType;
    private float bulletSpeed;
    private BulletType bulletType;
    private BulletDamageArea damageArea;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Destroy(gameObject);

    }

    public void DealDamage(Health targetHealth)
    {
        //TODO: calculate complex damage model
        targetHealth.inflictDamage(damage);
    }
}
