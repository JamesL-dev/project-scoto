/*
 * Filename: GreekFire.cs
 * Developer: Rodney McCoy
 * Purpose: Weapon Superclass instance to control GreekFire
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 */
public class GreekFire : Weapon
{
    void Start()
    {
        m_maxTime = 15;
        m_discovered = true;
    }

    /*
     * Function to instantiate greek fire
     */
    public override void Fire(Vector3 position, Quaternion rotation) 
    { 
        if(WeaponManager.AmmoAvailable())
        {
            Instantiate(m_projectile, position, rotation); 
            WeaponManager.DecrementAmmo();
        }
        else
        {
            Debug.Log("Greek Fire Out of Ammo");
        }
    }
}
