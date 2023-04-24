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
}
