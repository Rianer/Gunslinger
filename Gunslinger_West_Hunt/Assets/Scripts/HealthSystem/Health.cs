using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int startingHealth;
    private int currentHealth;

    public int StartingHealth
    {
        get { return startingHealth; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public void SetStartingHealth(int amount)
    {
        startingHealth = amount;
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Aplies damage to a game object that has a Health property
    /// </summary>
    /// <param name="amount">The amount of damage to be applied</param>
    /// <returns>true - the target is killed, false - otherwise</returns>
    public bool InflictDamage(ref int amount)
    {
        if (currentHealth <= 0)
        {
            return false;
        }
        if (amount >= currentHealth)
        {
            amount = amount - currentHealth;
            currentHealth = 0;
            return true;
        }

        currentHealth -= amount;
        amount = 0;
        return false;
    }

    /// <summary>
    /// Used to increase current health amount.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>If more healing is possible it returns True, False otherwise.</returns>
    public bool Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
            return false;
        }
        return true;
    }
}
