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
    public Health Health { get => health;}

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        health = new Health();
        armor = new Armor();
        health.SetStartingHealth(characterStats.healthPoints);
        armor.SetStartingArmor(characterStats.armorPoints);

        if (gameObject.CompareTag("Player"))
        {
            gm.playerHealthBar.ApplyMaxHealth(health.StartingHealth);
            UpdateGameManager();
        }
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
        if (gameObject.CompareTag("Player"))
        {
            UpdateGameManager();
        }
    }

    private void DebugStatus()
    {
        Debug.Log($"Health: {health.CurrentHealth}; Armor: {armor.CurrentArmor}");
    }

    private void UpdateGameManager()
    {
        gm.playerHealth = health.CurrentHealth;
        gm.playerArmor = armor.CurrentArmor;
        gm.playerHealthBar.SetHealth(health.CurrentHealth);
    }

    private void OnArmorBreak()
    {
        Debug.Log("Armor Broke");
    }

    private void OnZeroHealth()
    {
        if (!gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Unit Killed");

        }
        else
        {
            gm.IsPlayerAlive = false;
            Debug.Log("Player Killed");

        }
    }


}
