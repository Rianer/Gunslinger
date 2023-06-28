using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VitalityManager : MonoBehaviour
{

    [SerializeField]private CharacterStatsSO characterStats;
    private Health health;
    private Armor armor;
    public bool isTarget;
    public CharacterStatsSO CharacterStats { get => characterStats;}
    public Health Health { get => health;}

    private GameManager gm;
    private DateTime lastHitTime = new DateTime();
    private DateTime lastHealTime;

    [SerializeField] private List<GameObject> droppableItems = new List<GameObject>();

    private void Start()
    {
        lastHealTime = DateTime.Now;
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

    private void Update()
    {
        if(armor.CurrentArmor < armor.StartingArmor && characterStats.allowArmorHeal)
        {
            if (TryArmorHeal())
            {
                Debug.Log($"Armor Healed by {characterStats.armorHealAmmount} points!");
            }
        }
    }

    public void ReceiveDamage(int amount)
    {
        lastHitTime = DateTime.Now;
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

    public void ReceiveDamage(int amount, DamageType type)
    {
        lastHitTime = DateTime.Now;
        if (armor.InflictDamage(ref amount))
        {
            OnArmorBreak();
        }
        int effectiveDamage = CalculateRealDamage(amount, type);
        if (health.InflictDamage(ref effectiveDamage))
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
        gm.shieldBar.SetShield(armor.CurrentArmor);
    }

    private void OnArmorBreak()
    {
        Debug.Log("Armor Broke");
    }

    private void OnZeroHealth()
    {
        if (!gameObject.CompareTag("Player"))
        {
            if (isTarget)
            {
                gm.remainingTargets--;
                gm.CheckWinCondition();
            }
            DropItem();
            Destroy(gameObject);
            Debug.Log("Unit Killed");

        }
        else
        {
            gm.IsPlayerAlive = false;
            Debug.Log("Player Killed");
            gm.KillPlayer();
        }
    }

    private int CalculateRealDamage(int damage, DamageType type)
    {
        if(type == DamageType.elemental)
        {
            return (int)(damage - damage * characterStats.elementalResistance);
        }
        if(type == DamageType.kinetic)
        {
            return (int)(damage - damage * characterStats.kineticResistance);
        }
        if(type == DamageType.spiritual)
        {
            return (int)(damage - damage * characterStats.spiritualResistance);
        }
        return 0;
    }

    private bool TryArmorHeal()
    {
        if((DateTime.Now - lastHitTime).TotalMilliseconds <= characterStats.armorHealCooldown)
        {
            return false;
        }
        if ((DateTime.Now - lastHealTime).TotalMilliseconds <= characterStats.armorHealRate)
        {
            return false;
        }

        armor.Repair(characterStats.armorHealAmmount);
        lastHealTime = DateTime.Now;
        UpdateGameManager();

        return true;
    }

    private void DropItem()
    {
        if(droppableItems.Count == 0)
        {
            return;
        }

        var random = new System.Random();
        int index = random.Next(droppableItems.Count);
        Instantiate(droppableItems[index], gameObject.transform.position, gameObject.transform.rotation);
    }
}
