using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponStats_", menuName = "Weapon/Ranged")]
public class WeaponStatsSO : ScriptableObject
{
    public int damage;
    public DamageType damageType;
    [HideInInspector]public WeaponAttackType attackType;
    public bool singleFire = true;
    public float fireRate = 1;
    public float bulletSpeed = 0;
    public BulletDamageArea damageArea = BulletDamageArea.single;
    public BulletType bulletType = BulletType.common;
}
