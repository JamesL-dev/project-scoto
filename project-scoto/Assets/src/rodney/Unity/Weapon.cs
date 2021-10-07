using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int MAX_TIME = 15;
    [SerializeField] protected float Damage = 0;
    protected bool IsActive = false;
    protected bool discovered = false;

    public GameObject weapon, projectile;

    public bool isDiscovered() { return discovered; }
    public bool isActive() { return IsActive; }
    public bool isFound() { return discovered; }
    public int Time() { return MAX_TIME; }

    public void Found() 
    { 
        if(discovered) { Debug.LogError("Found() called on a weapon already discovered."); } 
        discovered = true; 
    }

    public void NotFound()
    {
        Debug.LogError("NotFound() called on a weapon. Use for tests and debugging only!");
        discovered = false;
    }
    
    public void setActive(bool yes) 
    { 
        weapon.SetActive(yes); 
        IsActive = yes; 
    }

    public virtual void Fire(Vector3 position, Quaternion rotation) 
    { 
        Instantiate(projectile, position, rotation); 
    }
}
