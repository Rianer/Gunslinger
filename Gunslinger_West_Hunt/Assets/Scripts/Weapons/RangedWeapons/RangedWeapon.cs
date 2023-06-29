using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class RangedWeapon : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private WeaponStatsSO weaponStats;

    /// <summary>
    /// Time in miliseconds that it takes for the weapon to prepare for shooting the next round
    /// </summary>
    private int timeDelayBetweenShots = 0;
    private DateTime shotLastTime;
    private bool isWeaponReloading;
    private int reloadTime_MS;
    private int bulletsInMagazine;
    private DateTime reloadStartTime;

    private void Awake()
    {
        isWeaponReloading = false;
        shotLastTime = new DateTime();
        weaponStats.attackType = WeaponAttackType.ranged;
        //Transforming RPM to time delay in miliseconds between shots
        timeDelayBetweenShots = (int)Math.Floor(60 / weaponStats.fireRate * 1000);
        reloadTime_MS = (int)Math.Floor(weaponStats.reloadTime_S * 1000);
        bulletsInMagazine = weaponStats.magazineSize;
        
    }
    public override void Attack()
    {
        if(bulletsInMagazine > 0)
            ExecuteFireCommand();
        CheckWeaponReloading();
    }

    private void Update()
    {
        if (isWeaponReloading)
        {
            if ((DateTime.Now - reloadStartTime).TotalMilliseconds >= reloadTime_MS)
            {
                isWeaponReloading=false;
                bulletsInMagazine = weaponStats.magazineSize;
                NotifyGameManager();
            }
        }
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

    public virtual void ExecuteFireCommand()
    {
        if(CheckTimeElapsed(timeDelayBetweenShots))
        {
            GameObject instantiatedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = instantiatedBullet.GetComponent<Bullet>();
            bullet.ApplyProperties(weaponStats);
            bullet.Fire(firePoint.up);
            bulletsInMagazine -= 1;
            NotifyGameManager();
        }
    }

    protected void CheckWeaponReloading()
    {
        if(bulletsInMagazine <= 0)
        {
            if (!isWeaponReloading) //if the weapon was not already reloading then start reloading
            {
                reloadStartTime = DateTime.Now;
                isWeaponReloading = true;
                NotifyGameManager();
            }
        }
    }


    protected bool CheckTimeElapsed(int milliseconds)
    {
        DateTime now = DateTime.Now;
        if ((now - shotLastTime).TotalMilliseconds >= milliseconds)
        {
            shotLastTime = now;
            return true;
        }
        return false;
    }

    public override void NotifyGameManager()
    {
        if (!isPlayerWeapon)
            return;
        WeaponStatus status = new WeaponStatus(weaponStats.magazineSize, bulletsInMagazine, isWeaponReloading);
        GameManager.Instance.UpdateWeaponUI(status);
    }
}
