using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour 
{
    public bool isPlayerWeapon = false;
    abstract public void Attack();
    abstract public void NotifyGameManager();
    
}
