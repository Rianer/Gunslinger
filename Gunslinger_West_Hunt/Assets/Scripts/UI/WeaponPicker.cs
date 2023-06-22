using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPicker : MonoBehaviour
{
    public string weaponName;
    public GameObject weaponPrefab;
    public int weaponPrice;
    public TextMeshProUGUI weaponDescription;
    public bool isUnlocked;
    public LoadoutSelector loadoutSelector;

    private Image weaponImage;
    private void Start()
    {
        CheckUnlock();
        weaponImage = GetComponent<Image>();
    }

    private void BuyWeapon()
    {
        if (isUnlocked) return;
        if(loadoutSelector.BuyWeapon(weaponName, weaponPrice))
            CheckUnlock();
    }

    private void EquipWeapon()
    {
        if (isUnlocked)
        {
            loadoutSelector.EquipWeapon(weaponPrefab, weaponImage, weaponName);
        }
    }

    private void CheckUnlock()
    {
        isUnlocked = false;
        weaponDescription.text = "Buy Weapon";
        if (loadoutSelector.weaponUnlocks.unlockedWeapons.Contains(weaponName))
        {
            isUnlocked = true;
            weaponDescription.text = "Equip";
        }
    }


    public void HandleClick()
    {
        if (isUnlocked)
        {
            EquipWeapon();
        }
        else
        {
            BuyWeapon();
        }
    }
}
