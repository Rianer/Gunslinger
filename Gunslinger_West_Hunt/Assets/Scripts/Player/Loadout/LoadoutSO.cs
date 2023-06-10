using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Loadout_", menuName = "Scriptable Objects/Loadout/New Loadout")]
public class LoadoutSO : ScriptableObject
{
    public GameObject gunPrefab;
    public string test;
}
