using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour, IObserver<InputObserverArgs>
{
    private Vector2 crosshairCoordinates;
    private ClickedButtons clickedButtons;
    private Weapon equipedWeapon;
    [SerializeField] private Transform weaponAnchorPoint = null;
    private bool allowAttack = true;

    public void EquipWeapon()
    {
        GameObject weaponObject = weaponAnchorPoint.GetChild(0).gameObject;
        equipedWeapon = weaponObject.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        equipedWeapon.NotifyGameManager();
    }

    public void UpdateObserver(InputObserverArgs args)
    {
        clickedButtons = args.ClickedButtons;
        crosshairCoordinates = args.MousePosition;

        if (equipedWeapon is RangedWeapon weapon)
        {
            weapon.AimWeapon(crosshairCoordinates);
        }

        if (!clickedButtons.fire)
        {
            allowAttack = true;
        }

        PerformAction();
    }

    public void SetEquipedWeapon(Weapon weapon)
    {
        equipedWeapon = weapon;
    }

    private void PerformAction()
    {
        if (clickedButtons.fire && allowAttack && equipedWeapon != null)
        {
            equipedWeapon.Attack();
            equipedWeapon.NotifyGameManager();
            allowAttack = false;
        }
    }
}
