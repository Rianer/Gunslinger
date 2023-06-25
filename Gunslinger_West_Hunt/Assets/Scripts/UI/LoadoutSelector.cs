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
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    private void Start()
    {
        UpdateMoneyDisplay();
        selectedWeaponImage.sprite = playerLoadout.equipedWeaponImage;
        selectedWeaponName.text = playerLoadout.equipedWeaponName;
        if(!weaponUnlocks.unlockedWeapons.Contains(playerLoadout.equipedWeaponName))
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
        UpdateMoneyDisplay();
        if (!weaponUnlocks.unlockedWeapons.Contains(playerLoadout.equipedWeaponName))
            weaponUnlocks.unlockedWeapons.Add(weaponName);
        return true;
    }


    private void UpdateMoneyDisplay()
    {
        string value = playerLoadout.playerMoney.ToString();
        moneyDisplay.text = $"${value}";
    }

    public int GetPlayerMoney()
    {
        return playerLoadout.playerMoney;
    }

    //public string GetEquipedWeaponName()
    //{
    //    return playerLoadout.equipedWeaponName;
    //}
}
