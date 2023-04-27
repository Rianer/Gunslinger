using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : Item
{
    [SerializeField] private int armor;
    public int Armor
    {
        get { return armor; }
    }
}
