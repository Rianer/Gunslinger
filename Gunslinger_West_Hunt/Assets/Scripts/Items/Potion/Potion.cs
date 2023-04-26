using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{

    [SerializeField] private int healthPoints;
    public int HealthPoints
    {
        get { return healthPoints; }
    }

    protected override void ApplyDetails()
    {
        base.ApplyDetails();
        healthPoints = details.healthPoints;
    }

    public override void PickUp()
    {
    }

    public override void Throw()
    {
    }
}
