using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // FOR DEBUGGING ONLY

public class WeaponManager : MonoBehaviour 
{
    int CurrentWeapon = 0, timer = 0, previous_index = 0, InvSize = 4;
    [SerializeField] public int FireAmount = 0;
    
    [SerializeField] bool FiringWeapons = false, ShowWeapons = true;
    bool SwapShowWeapons = false, error = false;

    private WeaponInputActions weapon_input_actions;
    private InputAction FireWeapon, ChangeWeapon, one, two, three, four;

    public Weapon[] weapon = new Weapon[4];

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
        if(!FiringWeapons && ShowWeapons) 
        {

            // IF ABLE TO FIRE WEAPONS 
            if(FireWeapon.ReadValue<float>() != 0) 
            {
                GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
                weapon[CurrentWeapon].Fire(Object.transform.position, Object.transform.rotation);
                timer = weapon[CurrentWeapon].Time();    
                FiringWeapons = true;
            }
            previous_index = CurrentWeapon;
            float ChangeWeaponVal;
            if((ChangeWeaponVal = ChangeWeapon.ReadValue<float>()) != 0)
            {
                // CHANGE INVENTORY SLOT BY "SCROLL"
                if(ChangeWeaponVal < 0) 
                { 
                    CurrentWeapon ++;
                    if (CurrentWeapon >= InvSize) {CurrentWeapon = 0;}
                    error = false;
                    while(!weapon[CurrentWeapon].isFound())
                    {
                        CurrentWeapon ++;
                        if (CurrentWeapon >= InvSize) 
                        {
                            CurrentWeapon = 0; 
                            if(error) 
                            {
                                Debug.LogError("No weapon in inventory is set to discovered, this can't happen. Setting weapon[0] to discovered.");
                                weapon[0].Found();
                                break;
                            }
                            error = true;
                        }
                    }
                }
                if(ChangeWeaponVal > 0) 
                {
                    CurrentWeapon --;
                    if (CurrentWeapon < 0) {CurrentWeapon = InvSize-1;}
                    error = false;
                    while(!weapon[CurrentWeapon].isFound())
                    {
                        CurrentWeapon --;
                        if (CurrentWeapon < 0) 
                        {
                            CurrentWeapon = InvSize-1;
                            if(error)
                            {
                                Debug.LogError("No weapon in inventory is set to discovered, this can't happen. Setting weapon[0] to discovered.");
                                weapon[0].Found();
                                break;
                            } 
                            error = true;                      
                        }
                    }
                }

                // SWITCH WHICH MODEL IS ACTIVATED
                weapon[previous_index].setActive(false);
                weapon[CurrentWeapon].setActive(true);
            }
            else
            {
                // CHANGE INVENTORY SLOT BY "HOTKEY"
                ChangeWeaponVal = 0;
                if (one.ReadValue<float>() != 0) {CurrentWeapon = 0; ChangeWeaponVal = 1;}
                if (two.ReadValue<float>() != 0) {CurrentWeapon = 1; ChangeWeaponVal = 1;}
                if (three.ReadValue<float>() != 0) {CurrentWeapon = 2; ChangeWeaponVal = 1;}
                if (four.ReadValue<float>() != 0) {CurrentWeapon = 3; ChangeWeaponVal = 1;}

                // SWITCH WHICH MODEL IS ACTIVATED
                if(ChangeWeaponVal == 1) 
                {
                    if(weapon[CurrentWeapon].isFound())
                    {
                        weapon[previous_index].setActive(false);
                        weapon[CurrentWeapon].setActive(true);
                    }
                    else
                    {
                        CurrentWeapon = previous_index;
                    }
                }
                else
                {
                    // NO USER INPUTTED INVENTORY CHANGE
                    if(CurrentWeapon >= InvSize || CurrentWeapon < 0)
                    {
                        CurrentWeapon = previous_index;
                    }
                }

                // ERROR CHECK
                error = false;
                while(!weapon[CurrentWeapon].isFound())
                    {
                        CurrentWeapon ++;
                        if (CurrentWeapon >= InvSize) 
                        {
                            CurrentWeapon = 0; 
                            if(error) 
                            {
                                Debug.LogError("No weapon in inventory is set to discovered, this can't happen. Setting weapon[0] to discovered.");
                                weapon[0].Found();
                                break;
                            }
                            error = true;
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
        if(SwapShowWeapons) 
        {
            if(ShowWeapons) { weapon[CurrentWeapon].setActive(false); } 
            else { weapon[CurrentWeapon].setActive(true); }
            ShowWeapons = !ShowWeapons;
            SwapShowWeapons = false;
        }
    }

    // SWITCH FOR EXTERNAL ENTITY TO NOTIFY SCRIPT THAT THEY WANT TO NOT DISPLAY WEAPONS
    public void EnableInventory(bool flag) { if(flag != ShowWeapons) { SwapShowWeapons = true; } }

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
}