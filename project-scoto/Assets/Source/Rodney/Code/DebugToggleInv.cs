/*
 * Filename: DebugToggleInv.cs
 * Developer: Rodney McCoy
 * Purpose: Debugging script to make weapons leave screen in game scene
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Main Class
 * 
 * Member Variables:
 * m_timer -- timer tell state can be switched
 * m_currState -- whether the inventory is currently showing
 * m_weaponInputActions -- for user input
 * m_debugging -- for use input
 */
public class DebugToggleInv : MonoBehaviour
{
    [SerializeField] public int m_timer = 0;
    [SerializeField] public bool m_currState = true;
    
    private WeaponInputActions m_weaponInputActions;
    private InputAction m_debugging;

    void Awake()
    { 
        m_weaponInputActions = new WeaponInputActions();
        m_debugging = m_weaponInputActions.Player.DEBUGGING;
    }

    void FixedUpdate()
    {
        if(m_timer > 0) {m_timer --;}

        if(m_debugging.ReadValue<float>() != 0 && m_timer == 0) 
        {
            /* This line is what allows you to turn the inventory on and off. The first part gives access to the function EnableInventory.
            The second part, EnableInventory(bool var), turns the inventory ON if var is true (and if the inventory is currently off) and it turns the
            inventory OFF if var is false (and the inventory is currently on).
            Feel free to use this line in code if needed but i might be changing it to be more simple, by using a namespace,
            so be wary of if i change, which i will notify discord if i do */
            GameObject.Find("Inventory").GetComponent<WeaponManager>().EnableInventory(!m_currState);
            
            m_currState = !m_currState; // Keep track of current state of inventory
            m_timer = 60; // Only toggle inventory every 60 frames
        }
    }

    private void OnEnable() { m_debugging.Enable(); }

    private void OnDisable() { m_debugging.Disable(); }
}

