using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUnlocks", menuName = "Scriptable Objects/Loadout/Weapon Unlocks")]
public class WeaponUnlocksSO : ScriptableObject
{
    public List<string> unlockedWeapons;

}
