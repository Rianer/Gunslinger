using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutSelector : MonoBehaviour
{
    [SerializeField] private LoadoutSO playerLoadout;
    public WeaponUnlocksSO weaponUnlocks;
    [SerializeField] private Image selectedWeaponImage;
    [SerializeField] private TextMeshProUGUI selectedWeaponName;

    private void Start()
    {
        selectedWeaponImage.sprite = playerLoadout.equipedWeaponImage;
        selectedWeaponName.text = playerLoadout.equipedWeaponName;
        weaponUnlocks.unlockedWeapons.Add(playerLoadout.equipedWeaponName);
    }
    public void EquipWeapon(GameObject newWeapon, Image image, string name)
    {
        playerLoadout.gunPrefab = newWeapon;
        selectedWeaponImage.sprite = image.sprite;
        selectedWeaponName.text = name;
        playerLoadout.equipedWeaponImage = image.sprite;
        playerLoadout.equipedWeaponName = name;
    }

    public bool BuyWeapon(string weaponName, int price)
    {
        if (weaponUnlocks.unlockedWeapons.Contains(weaponName))
            return false;

        if(price > playerLoadout.playerMoney)
            return false;

        playerLoadout.playerMoney -= price;
        weaponUnlocks.unlockedWeapons.Add(weaponName);
        return true;
    }
}
