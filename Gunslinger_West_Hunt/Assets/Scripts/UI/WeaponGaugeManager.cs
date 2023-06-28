using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponGaugeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoIndicator;


    public void UpdateAmmoIndicator(string value)
    {
        ammoIndicator.text = value;
    }
}
