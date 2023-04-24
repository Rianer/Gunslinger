using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandler : MonoBehaviour, IObserver<InputObserverArgs>
{
    private ClickedButtons clickedButtons;
    [SerializeField]private Weapon equipedWeapon = null;
    private bool allowAttack = true;

    public void UpdateObserver(InputObserverArgs args)
    {
        clickedButtons = args.ClickedButtons;
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
            allowAttack = false;
        }
    }
}
