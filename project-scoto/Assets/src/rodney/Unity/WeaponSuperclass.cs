using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSuperclass : MonoBehaviour
{
    protected int Damage = 0;
    protected int MAX_TIME = 15;
    protected bool IsActive = false;
    
    public GameObject weapon = null;
    public GameObject projectile = null;

    public void setActive(bool yes) {
        // Debug.Log("WeaponSuperclass: " + yes.ToString());
        weapon.SetActive(yes);
        IsActive = yes;
    }

    public bool isActive() { return IsActive; }

    public int Time() { return MAX_TIME; }

    public void SpawnProjectile(Vector3 position, Quaternion rotation) {
        Instantiate(projectile, position, rotation);
    }
}
