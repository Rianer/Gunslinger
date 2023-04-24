using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
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
    public bool inflictDamage(int amount)
    {
        if(amount >= currentHealth)
        {
            currentHealth = 0;
            return true;
        }

        currentHealth -= amount;
        return false;
    }

    
}
