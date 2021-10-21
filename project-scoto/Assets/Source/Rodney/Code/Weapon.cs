/*
 * Filename: Weapon.cs
 * Developer: Rodney McCoy
 * Purpose: Weapon Superclass
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 *
 * Member Variables:
 * m_weapon -- instance of weapon prefab , for activating / deactivating
 * m_projectile -- instance of projectile prefab for instantiating projectile
 * m_damage -- damage tied to the weapon
 * m_maxTime -- time for weapon to fire
 * m_isActive -- tracks if 3d model is active on screen or not
 * m_discovered -- tracks if weapon has been discovered or not
 */
public class Weapon : MonoBehaviour
{
    [SerializeField] public float m_damage = 10;
    public GameObject m_weapon, m_projectile;

    protected int m_maxTime = 15;
    protected bool m_isActive = false, m_discovered = false;

    /*
     * Accessor Functions. Your dumb if you need more info to understand these.
     * 
     * Returns:
     * Specific value its an accessor for
     */
    public bool isDiscovered() { return m_discovered; }
    public bool isActive() { return m_isActive; }
    public bool isFound() { return m_discovered; }
    public int Time() { return m_maxTime; }
    
    /*
     * Sets a weapon to discovered, so player can use it
     */
    public void Found() 
    { 
        if(m_discovered) { Debug.LogWarning("Found() called on a weapon already m_discovered."); } 
        m_discovered = true; 
    }

    /*
     * Sets a weapon to not discovered, so player cant use it
     */
    public void NotFound()
    {
        Debug.LogWarning("Function NotFound() only to be used for testing & debugging.");
        m_discovered = false;
    }
    
    /*
     * Sets the weapon 3d model to active in the game scene
     */
    public void setActive(bool yes) 
    { 
        m_weapon.SetActive(yes); 
        m_isActive = yes; 
    }

    /*
     * Function to fire the weapon (overloaded at each instance of a subclass)
     */
    public virtual void Fire(Vector3 position, Quaternion rotation) 
    { 
        Instantiate(m_projectile, position, rotation); 
    }
}

