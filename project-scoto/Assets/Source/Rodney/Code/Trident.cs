/*
 * Filename: Trident.cs
 * Developer: Rodney McCoy
 * Purpose: Weapon Superclass instance to control trident
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 */
public class Trident : Weapon 
{
    
    void Start() 
    {
        m_maxTime = 30; 
        m_discovered = true;
        m_damage = 33;
    }

    /*
     * overriden function used to fire weapon
     * 
     * Parameters:
     * position -- position vector for raycast
     * rotation -- rotation quaternion for direction of raycast
     */
    public override void Fire(Vector3 position, Quaternion rotation) 
    { 
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100);

        BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hit.collider);
        if (enemy) { enemy.HitEnemy(BaseEnemy.WeaponType.Trident, m_damage); }
    }
}

