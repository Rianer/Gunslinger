using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : Item
{
    [SerializeField] private int experiencePoints;

    public int ExperiencePoints
    {
        get { return experiencePoints; }
    }

    protected override void ApplyDetails()
    {
        base.ApplyDetails();
        experiencePoints = details.experiencePoints;
    }

}
