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
    float m_velocityScalar = 20F;

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
            GameObject thisProjectile = m_objPool.acquireReusable(ProjectileObjectPool.ProjectileType.Grenade);
            thisProjectile.transform.position = position;
            thisProjectile.transform.rotation = rotation;
            thisProjectile.GetComponent<Rigidbody>().velocity = gameObject.transform.rotation*Quaternion.Euler(80,0,0) * Vector3.up * m_velocityScalar;
            thisProjectile.SetActive(true);

            WeaponManager.DecrementAmmo();
        }
        else
        {
            Debug.Log("Greek Fire Out of Ammo");
        }
    }
}

