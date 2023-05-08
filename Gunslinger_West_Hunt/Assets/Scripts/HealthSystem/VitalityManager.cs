using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VitalityManager : MonoBehaviour
{

    [SerializeField]private CharacterStatsSO characterStats;
    private Health health;
    private Armor armor;

    public CharacterStatsSO CharacterStats { get => characterStats;}


    private void Start()
    {
        health = new Health();
        armor = new Armor();
        health.SetStartingHealth(characterStats.healthPoints);
        armor.SetStartingArmor(characterStats.armorPoints);
    }

    public void ReceiveDamage(int amount)
    {
        if (armor.InflictDamage(ref amount))
        {
            OnArmorBreak();
        }
        if (health.InflictDamage(ref amount))
        {
            OnZeroHealth();
        }
        DebugStatus();
    }

    private void DebugStatus()
    {
        Debug.Log($"Health: {health.CurrentHealth}; Armor: {armor.CurrentArmor}");
    }

    private void OnArmorBreak()
    {
        Debug.Log("Armor Broke");
    }

    private void OnZeroHealth()
    {
        Debug.Log("Unit Killed");
        Destroy(gameObject);
    }


}
