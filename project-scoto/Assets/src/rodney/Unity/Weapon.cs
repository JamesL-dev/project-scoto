using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int Damage = 0, MAX_TIME = 15;
    protected bool IsActive = false;
    
    [SerializeField]
    protected bool discovered;

    public GameObject weapon, projectile;

    public void setActive(bool yes) { weapon.SetActive(yes); IsActive = yes; }

    public bool isActive() { return IsActive; }
    public bool isFound() { return discovered; }
    public int Time() { return MAX_TIME; }

    public virtual void Fire(Vector3 position, Quaternion rotation) { Instantiate(projectile, position, rotation); }

    public void setDiscovered() 
    {
        if(discovered) { Debug.LogError("The setDiscovered() subroutine was called on an object with weapon superclass, but it is already discoered. No change will occur."); }
        discovered = true;
        return;
    }
}
