using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour 
{
    public const int InvSize = 4, initWeapon = 0;
    int CurrWeapon = initWeapon, previous_index = initWeapon, ChangeCurrWeapon = -1, timer = 0;
    [SerializeField] public int FireAmount = 0;
    
    [SerializeField] bool FiringWeapons = false, ShowWeapons = true;
    bool ChangeShowWeapons = false;

    private WeaponInputActions weapon_input_actions;
    private InputAction FireWeapon, ChangeWeapon, one, two, three, four;

    public Weapon[] weapon = new Weapon[InvSize];

    private void Awake() 
    {
        weapon_input_actions = new WeaponInputActions();
        FireWeapon = weapon_input_actions.Player.FireWeapon;
        ChangeWeapon = weapon_input_actions.Player.ChangeWeapon;

        one = weapon_input_actions.Player.one;
        two = weapon_input_actions.Player.two;
        three = weapon_input_actions.Player.three;
        four = weapon_input_actions.Player.four;

        weapon[0].setActive(true);
        for(int i = 1; i < InvSize; i++) { weapon[i].setActive(false); }
    }

    void FixedUpdate() 
    {
        // ERROR HANDLING
        // IF ALL WEAPONS ON NOT DISCOVERED, SET weapon[initWeapon] to discovered
        bool flag = false;
        for(int i = 0; i < InvSize; i++) { if(weapon[i].isFound()) {flag = true; break;} }
        if(!flag) {Debug.LogWarning("No weapon in inventory is set to discovered, this can't happen. Setting weapon[initWeapon] to discovered."); weapon[initWeapon].Found();}
        // IF CURRENT WEAPON OUT OF BOUNDS, SET TO PREVIOUS VALUE 
        if(CurrWeapon >= InvSize || CurrWeapon < 0) { CurrWeapon = previous_index; }
        

        if(!FiringWeapons && ShowWeapons) 
        {
            // IF ABLE TO FIRE WEAPONS 
            if(FireWeapon.ReadValue<float>() != 0) 
            {
                GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
                weapon[CurrWeapon].Fire(Object.transform.position, Object.transform.rotation);
                timer = weapon[CurrWeapon].Time();    
                FiringWeapons = true;
            }
            previous_index = CurrWeapon;
            float ChangeWeaponVal;

            if((ChangeWeaponVal = ChangeWeapon.ReadValue<float>()) != 0)
            {
                // CHANGE INVENTORY SLOT BY "SCROLL"
                if(ChangeWeaponVal < 0) 
                { 
                    CurrWeapon ++;
                    if (CurrWeapon >= InvSize) {CurrWeapon = 0;}
                }
                if(ChangeWeaponVal > 0) 
                {
                    CurrWeapon --;
                    if (CurrWeapon < 0) {CurrWeapon = InvSize-1;}
                }
                // SWITCH WHICH MODEL IS ACTIVATED
                weapon[previous_index].setActive(false);
                weapon[CurrWeapon].setActive(true);
            }
            else
            {
                // CHANGE INVENTORY SLOT BY "HOTKEY"
                ChangeWeaponVal = 0;
                if (one.ReadValue<float>() != 0) {CurrWeapon = 0; ChangeWeaponVal = 1;}
                if (two.ReadValue<float>() != 0) {CurrWeapon = 1; ChangeWeaponVal = 1;}
                if (three.ReadValue<float>() != 0) {CurrWeapon = 2; ChangeWeaponVal = 1;}
                if (four.ReadValue<float>() != 0) {CurrWeapon = 3; ChangeWeaponVal = 1;}
                // SWITCH WHICH MODEL IS ACTIVATED
                if(ChangeWeaponVal == 1) 
                {
                    if(weapon[CurrWeapon].isFound() && CurrWeapon != previous_index)
                    {
                        weapon[previous_index].setActive(false);
                        weapon[CurrWeapon].setActive(true);
                    }
                    else
                    {
                        CurrWeapon = previous_index;
                    }
                }
            }
        } 
        else 
        { 
            // IF NOT ABLE TO FIRE WEAPONS
            if(timer > 0) { timer --; }
            if(timer == 0) { FiringWeapons = false; }
        }


        // IF EXTERNAL ENTITY WANTS TO NOT DISPLAY WEAPONS
        if(ChangeShowWeapons) 
        {
            if(ShowWeapons) { weapon[CurrWeapon].setActive(false); } 
            else { weapon[CurrWeapon].setActive(true); }
            ShowWeapons = !ShowWeapons;
            ChangeShowWeapons = false;
        }

        // IF EXTERNAL ENTITY WANTS TO CHANGE CURRENT WEAPON
        if(ChangeCurrWeapon != -1)
        {
            CurrWeapon = ChangeCurrWeapon;
            ChangeCurrWeapon = -1;
        }
    }


    // SWITCH FOR EXTERNAL ENTITY TO NOTIFY SCRIPT THAT THEY WANT TO NOT DISPLAY WEAPONS
    public void EnableInventory(bool flag) { if(flag != ShowWeapons) { ChangeShowWeapons = true; } }

    public void SetCurrentWeapon(int x)
    { 
        Debug.LogWarning("Function SetCurrentWeapon() only to be used for testing & debugging."); 
        if(x == -1) {Debug.LogWarning("SetCurrentWeapon() will not change CurrentWeapon because it was given \"-1\"");}
        ChangeCurrWeapon = x; 
    }

    private void OnEnable() 
    {
        FireWeapon.Enable();
        ChangeWeapon.Enable();
        one.Enable();
        two.Enable();
        three.Enable();
        four.Enable();
    }

    private void OnDisable() 
    {
        FireWeapon.Disable();
        ChangeWeapon.Disable();
        one.Disable();
        two.Disable();
        three.Disable();
        four.Disable();
    }

    public int CurrentWeapon() { return CurrWeapon;}
}