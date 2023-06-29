using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats_", menuName = "Scriptable Objects/Character/New Character Stats")]
public class CharacterStatsSO : ScriptableObject
{
    public int healthPoints;
    public int armorPoints;

    public float kineticResistance;
    public float elementalResistance;
    public float spiritualResistance;

    public float itemDropChance = 0;


    public bool allowArmorHeal;
    /// <summary>
    /// How many miliseconds shall pass after the last hit to begin armor regen
    /// </summary>
    public int armorHealCooldown;
    /// <summary>
    /// The rate at which the armor will start healing described as a time gap in miliseconds
    /// </summary>
    public int armorHealRate;
    /// <summary>
    /// Ammount of armor points added per each heal tick
    /// </summary>
    public int armorHealAmmount;

}
