using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : Item
{
    private int armor;
    public int Armor
    {
        get { return armor; }
    }

    protected override void ApplyDetails()
    {
        base.ApplyDetails();
        armor = details.armor;
    }
}
