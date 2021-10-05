using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int Damage = 0, MAX_TIME = 15;
    protected bool IsActive = false;
    
    public GameObject weapon, projectile;

    public void setActive(bool yes) { weapon.SetActive(yes); IsActive = yes; }

    public bool isActive() { return IsActive; }

    public int Time() { return MAX_TIME; }

    public virtual void Fire(Vector3 position, Quaternion rotation) { Instantiate(projectile, position, rotation); }
}
