using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class RangedWeapon : Weapon
{
    //[SerializeField] private Transform crossHair;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private WeaponStatsSO weaponStats;

    private void Awake()
    {
        weaponStats.attackType = WeaponAttackType.ranged;
    }
    public override void Attack()
    {
        GameObject instantiatedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = instantiatedBullet.GetComponent<Bullet>();
        bullet.ApplyProperties(weaponStats);
        bullet.Fire(firePoint.up);
    }

    public void AimWeapon(Vector2 crosshairCoordinates)
    {
        Vector2 lookDirection = crosshairCoordinates - new Vector2(transform.position.x, transform.position.y);
        if(lookDirection.magnitude > 1)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }
}
