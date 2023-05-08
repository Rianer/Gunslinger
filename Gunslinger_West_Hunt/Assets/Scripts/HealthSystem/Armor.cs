using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Armor
{
    private int startingArmor;
    private int currentArmor;

    public int StartingArmor { get => startingArmor; set => startingArmor = value; }
    public int CurrentArmor { get => currentArmor; set => currentArmor = value; }
    

    public void SetStartingArmor(int amount)
    {
        startingArmor = amount;
        currentArmor = startingArmor;
    }

    /// <summary>
    /// Applies damage to armor.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>If the armor gets destroyed it returns True, False otherwise.</returns>
    public bool InflictDamage(ref int amount)
    {
        if(currentArmor <= 0)
        {
            return false;
        }
        if (amount >= currentArmor)
        {
            amount = amount - currentArmor;
            currentArmor = 0;
            return true;
        }

        currentArmor -= amount;
        amount = 0;
        return false;
    }

    /// <summary>
    /// Used to increase current armour amount.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>If more repairing is possible it returns True, False otherwise.</returns>
    public bool Repair(int amount)
    {
        currentArmor += amount;
        if (currentArmor >= startingArmor)
        {
            currentArmor = startingArmor;
            return false;
        }
        return true;
    }
}
