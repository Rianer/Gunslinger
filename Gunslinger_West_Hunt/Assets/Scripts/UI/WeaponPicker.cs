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
    public bool isEquiped = false;
    public LoadoutSelector loadoutSelector;
    [TextArea(15, 20)]
    public string description;

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
        if(weaponPrice > loadoutSelector.GetPlayerMoney())
        {
            weaponDescription.text = "No Money";
        }
        else
        {
            weaponDescription.text = "Buy Weapon";
        }
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
            CheckUnlock();
        }
        else
        {
            BuyWeapon();
            CheckUnlock();
        }
    }

    public void ShowDescriprion()
    {
        loadoutSelector.UpdateWeaponDescription(description);
    }

    public void ClearDescription()
    {
        loadoutSelector.UpdateWeaponDescription("");
    }
}
