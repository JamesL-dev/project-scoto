/*
 * Filename: WeaponManager.cs
 * Developer: Rodney McCoy
 * Purpose: Singleton to manage and control the weapons the player can use. Implements dynamic binding.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Main Class
 *
 * Member Variables:
 * m_invSize -- size of inventory
 * m_initWeapon -- (zero indexed) which weapon should always be discovered
 * m_camera -- GameObject to obtain cameras transform (rotation)
 * m_weapon -- dynamically bound array of weapon subclass instances
 * m_currWeapon -- weapon currently selected
 * m_previousIndex -- weapon previously selected
 * m_changeCurrWeapon -- change current weapon from outside script (for testing only)
 * m_timer -- time tell weapon stops firing
 * m_fireAmount -- GreekFire ammo
 * m_firingWeapons -- true if currently firing a weapon
 * m_showWeapons -- true if no external entity has deactivated weapons
 * m_changeShowWeapons -- ture if external entity wants to deactivate weapons
 * m_weaponInputActions, m_fireWeapon, m_changeWeapon, m_one, m_two, m_three, m_four -- user input
 * m_instance -- Singleton Instance
 */
public sealed class WeaponManager : MonoBehaviour 
{  
    public const int m_invSize = 3, m_initWeapon = 0;
    public GameObject m_camera;
    [SerializeField] public Weapon[] m_weapon = new Weapon[m_invSize];    


    int m_currWeapon = m_initWeapon, m_previousIndex = m_initWeapon, m_changeCurrWeapon = -1, m_timer = 0;
    static int m_fireAmount = 120;
    bool m_firingWeapons = false, m_showWeapons = true, m_changeShowWeapons = false;
    private WeaponInputActions m_weaponInputActions;
    private InputAction m_fireWeapon, m_changeWeapon, m_one, m_two, m_three, m_four;

    private void Awake() 
    {
        m_weaponInputActions = new WeaponInputActions();
        m_fireWeapon = m_weaponInputActions.Player.FireWeapon;
        m_changeWeapon = m_weaponInputActions.Player.ChangeWeapon;
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");

        m_one = m_weaponInputActions.Player.one;
        m_two = m_weaponInputActions.Player.two;
        m_three = m_weaponInputActions.Player.three;
        m_four = m_weaponInputActions.Player.four;

        m_weapon[0].setActive(true);
        for(int i = 1; i < m_invSize; i++) { m_weapon[i].setActive(false); }
    }

    void FixedUpdate() 
    {
        // ERROR HANDLING
        // IF ALL WEAPONS ON NOT DISCOVERED, SET m_weapon[m_initWeapon] to discovered
        bool flag = false;
        for(int i = 0; i < m_invSize; i++) { if(m_weapon[i].isFound()) {flag = true; break;} }
        if(!flag) {Debug.LogWarning("No m_weapon in inventory is set to discovered, this can't happen. Setting m_weapon[m_initWeapon] to discovered."); m_weapon[m_initWeapon].Found();}
        // IF CURRENT WEAPON OUT OF BOUNDS, SET TO PREVIOUS VALUE 
        if(m_currWeapon >= m_invSize || m_currWeapon < 0) { m_currWeapon = m_previousIndex; }
        
        if(!m_showWeapons) {Demo.ResetTimer();}

        if(!m_firingWeapons && m_showWeapons) 
        {
            float ChangeWeaponVal = m_fireWeapon.ReadValue<float>();
            // IF ABLE TO FIRE WEAPONS 
            if(ChangeWeaponVal != 0 || (Demo.On() && Demo.Attack() != 0)) 
            {
                /* Main Use of Dynamic Binding. Implements virtual Fire functions overloaded at each subclass*/
                m_weapon[m_currWeapon].Fire(m_camera.transform.position, m_camera.transform.rotation);

                m_timer = m_weapon[m_currWeapon].Time();    
                m_firingWeapons = true;
                
                if(ChangeWeaponVal != 0){ Demo.ResetTimer(); }
            }
            m_previousIndex = m_currWeapon;

            if((ChangeWeaponVal = m_changeWeapon.ReadValue<float>()) != 0)
            {
                Demo.ResetTimer();
                // CHANGE INVENTORY SLOT BY "SCROLL"
                if(ChangeWeaponVal < 0) 
                { 
                    m_currWeapon ++;
                    if (m_currWeapon >= m_invSize) {m_currWeapon = 0;}
                }
                if(ChangeWeaponVal > 0) 
                {
                    m_currWeapon --;
                    if (m_currWeapon < 0) {m_currWeapon = m_invSize-1;}
                }
                // SWITCH WHICH MODEL IS ACTIVATED
                m_weapon[m_previousIndex].setActive(false);
                m_weapon[m_currWeapon].setActive(true);
            }
            else
            {
                // CHANGE INVENTORY SLOT BY "HOTKEY"
                ChangeWeaponVal = 0;
                if (m_one.ReadValue<float>() != 0) {m_currWeapon = 0; ChangeWeaponVal = 1;}
                if (m_two.ReadValue<float>() != 0) {m_currWeapon = 1; ChangeWeaponVal = 1;}
                if (m_three.ReadValue<float>() != 0) {m_currWeapon = 2; ChangeWeaponVal = 1;}
                // if (m_four.ReadValue<float>() != 0) {m_currWeapon = 3; ChangeWeaponVal = 1;}
                // SWITCH WHICH MODEL IS ACTIVATED
                if(ChangeWeaponVal == 1) 
                {
                    Demo.ResetTimer();
                    if(m_weapon[m_currWeapon].isFound() && m_currWeapon != m_previousIndex)
                    {
                        m_weapon[m_previousIndex].setActive(false);
                        m_weapon[m_currWeapon].setActive(true);
                    }
                    else
                    {
                        m_currWeapon = m_previousIndex;
                    }
                }
            }
        } 
        else 
        { 
            // IF NOT ABLE TO FIRE WEAPONS
            if(m_timer > 0) { m_timer --; }
            if(m_timer == 0) { m_firingWeapons = false; }
        }

        // IF EXTERNAL ENTITY WANTS TO NOT DISPLAY WEAPONS 
        if(m_changeShowWeapons) 
        {
            if(m_showWeapons) { m_weapon[m_currWeapon].setActive(false); } 
            else { m_weapon[m_currWeapon].setActive(true); }
            m_showWeapons = !m_showWeapons;
            m_changeShowWeapons = false;
        }

        // IF EXTERNAL ENTITY WANTS TO CHANGE CURRENT WEAPON
        if(m_changeCurrWeapon != -1)
        {
            m_currWeapon = m_changeCurrWeapon;
            m_changeCurrWeapon = -1;
        }

        if(Demo.On()) {CurrentWeapon = 2;}
    }

    public void OnEnable() 
    {
        m_fireWeapon.Enable();
        m_changeWeapon.Enable();
        m_one.Enable();
        m_two.Enable();
        m_three.Enable();
        m_four.Enable();
    }

    public void OnDisable() 
    {
        m_fireWeapon.Disable();
        m_changeWeapon.Disable();
        m_one.Disable();
        m_two.Disable();
        m_three.Disable();
        m_four.Disable();
    }

    /*
     * Switch for external entity to notify script to turn off weapons until further notified
     *
     * Parameters:
     * flag -- true to enable inventory, false to disable inventory
     */ 
    public void EnableInventory(bool flag) { if(flag != m_showWeapons) { m_changeShowWeapons = true; } }

    /*
     * Mutator to set current m_weapon. Only for testing
     *
     * Parameters:
     * x -- Current Weapon
     */
    public void SetCurrentWeapon(int x)
    { 
        Debug.LogWarning("Function SetCurrentWeapon() only to be used for testing & debugging."); 
        if(x == -1) {Debug.LogWarning("SetCurrentWeapon() will not change CurrentWeapon because it was given \"-1\"");}
        m_changeCurrWeapon = x; 
    }

    /*
     * Decrements GreekFire ammo
     */
    public static void DecrementAmmo() { m_fireAmount--; }

    /*
     * Is their greek fire ammo
     *
     * Returns:
     * bool -- true if greek fire ammo available
     */
    public static bool AmmoAvailable() { return m_fireAmount > 0; }

    /*
     * Accessor to CurrentWeapon
     */
    public int CurrentWeapon() { return m_currWeapon;}

    /* Members to created singleton design pattern */
    private static WeaponManager m_instance;
    public static WeaponManager Inst()
    {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("Inventory").GetComponent<WeaponManager>();
        }
        return m_instance;
    }
    private WeaponManager() {}
}

