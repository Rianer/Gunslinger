using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class RangedEnemyAI : MonoBehaviour
{
    private EnemyAI enemyAI;
    [SerializeField]private RangedWeapon weapon;
    [SerializeField] private int threatLevel;
    /// <summary>
    /// Must be a float between 0 and 1
    /// </summary>
    [SerializeField] private float agresionProbability;
    [SerializeField] private int shootingDelayMS;

    //Time Delta between shots
    DateTime shootLastTime;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.TargetDetected += OnPlayerDetected;
        shootLastTime = DateTime.Now;
    }

    private void OnPlayerDetected(object sender, TargetDetectedEventArgs e)
    {
        if (HelperUtilities.OfChance(agresionProbability))
        {
            if (CheckTimeElapsed(shootingDelayMS))
            {
                weapon.Attack();
            }

        }
        //Generate spread based on enemy threat level
        //Check if enough time has passed since last shot (mimick fire-rate)
        //Shoot
    }

    private bool CheckTimeElapsed(int milliseconds)
    {
        DateTime now = DateTime.Now;
        if ((now - shootLastTime).TotalMilliseconds >= milliseconds)
        {
            shootLastTime = now;
            return true;
        }
        return false;
    }


}
