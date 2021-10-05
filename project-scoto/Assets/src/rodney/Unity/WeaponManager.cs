using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // FOR DEBUGGING ONLY

public class WeaponManager : MonoBehaviour {
    public int CurrentWeapon = 1, FireAmount = 0;
    public bool BowFound = false, TridentFound = false;

    private WeaponInputActions weapon_input_actions;
    private InputAction FireWeapon, ChangeWeapon;
    private InputAction one, two, three, four;

    public Bow bow;
    public GreekFire greekFire;
    public Trident trident;
    public Inv_Flashlight flashlight;

    int timer = 0, previous_index = 1, InvSize = 4;
    bool FiringWeapons = false, ShowWeapons = true;
    bool SwapShowWeapons = false; // Determines if outside entity wants to swith EnableAttack

    private void Awake() {
        weapon_input_actions = new WeaponInputActions();
        FireWeapon = weapon_input_actions.Player.FireWeapon;
        ChangeWeapon = weapon_input_actions.Player.ChangeWeapon;

        one = weapon_input_actions.Player.one;
        two = weapon_input_actions.Player.two;
        three = weapon_input_actions.Player.three;
        four = weapon_input_actions.Player.four;

        bow.setActive(true);
        greekFire.setActive(false);
        trident.setActive(false);                
        flashlight.setActive(false);
    }

    private void OnEnable() {
        FireWeapon.Enable();
        ChangeWeapon.Enable();
        one.Enable();
        two.Enable();
        three.Enable();
        four.Enable();
    }

    private void OnDisable() {
        FireWeapon.Disable();
        ChangeWeapon.Disable();
        one.Disable();
        two.Disable();
        three.Disable();
        four.Disable();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!FiringWeapons && ShowWeapons) {
            // FIRING WEAPONS 
            if(FireWeapon.ReadValue<float>() != 0) {
                switch(CurrentWeapon) {
                    case 1:
                        FireBow();
                        timer = bow.Time();
                        break;
                    case 2:
                        FireGreek();
                        timer = greekFire.Time();
                        break;
                    case 3:
                        // DO NOTHING
                        break;
                    case 4:
                        // FLASHLIGHT ACTIVATION FUNCTION
                        break;
                    default:
                        break;
                }
                FiringWeapons = true;
            }
            
            // CHANGE INVENTORY SLOT (SCROLL)
            float ChangeWeaponVal = ChangeWeapon.ReadValue<float>();
            bool WeaponChanged = false;
            previous_index = CurrentWeapon;
            if(ChangeWeaponVal != 0) {WeaponChanged = true;}
            if(ChangeWeaponVal < 0) { 
                CurrentWeapon ++;
                if (CurrentWeapon > InvSize) {CurrentWeapon = 1;}
            }
            if(ChangeWeaponVal > 0) {
                CurrentWeapon --;
                if (CurrentWeapon <= 0) {CurrentWeapon = InvSize;}
            }

            // CHANGE INVENTORY SLOT (HOTKEYS)
            if (one.ReadValue<float>() != 0) {CurrentWeapon = 1; WeaponChanged = true;}
            if (two.ReadValue<float>() != 0) {CurrentWeapon = 2; WeaponChanged = true;}
            if (three.ReadValue<float>() != 0) {CurrentWeapon = 3; WeaponChanged = true;}
            if (four.ReadValue<float>() != 0) {CurrentWeapon = 4; WeaponChanged = true;}
            
            // CHANGE DISPLAYED WEAPON / ITEM
            if(WeaponChanged) {
                switch(previous_index) {
                    case 1:
                        bow.setActive(false);
                        break;
                    case 2:
                        greekFire.setActive(false);
                        break;
                    case 3:
                        trident.setActive(false);
                        break;     
                    case 4:
                        flashlight.setActive(false);
                        break;
                    default:
                        Debug.LogError("CurrentWeapon is out of expected range");
                        break;
                }
                switch(CurrentWeapon) {
                    case 1:
                        bow.setActive(true);
                        break;
                    case 2:
                        greekFire.setActive(true);
                        break;
                    case 3:
                        trident.setActive(true);
                        break;      
                    case 4:
                        flashlight.setActive(true);
                        break;
                    default:
                        Debug.LogError("CurrentWeapon is out of expected range");
                        break;
                }
            }
        } else { // IF ENABLE ATTACK IS FALSE
            if(timer > 0) { timer --; }
            if(timer == 0) { FiringWeapons = false; }
        }

        if(SwapShowWeapons) {
            if(ShowWeapons) {
                switch(CurrentWeapon) {
                    case 1:
                        bow.setActive(false);
                        break;
                    case 2:
                        greekFire.setActive(false);
                        break;
                    case 3:
                        trident.setActive(false);
                        break;      
                    case 4:
                        flashlight.setActive(false);
                        break;
                    default:
                        Debug.LogError("CurrentWeapon is out of expected range");
                        break;
                }
            } else {
                switch(CurrentWeapon) {
                    case 1:
                        bow.setActive(true);
                        break;
                    case 2:
                        greekFire.setActive(true);
                        break;
                    case 3:
                        trident.setActive(true);
                        break;      
                    case 4:
                        flashlight.setActive(true);
                        break;
                    default:
                        Debug.LogError("CurrentWeapon is out of expected range");
                        break;
                }
            }
            ShowWeapons = !ShowWeapons;
            SwapShowWeapons = false;
        }
    }

    public void EnableInventory(bool flag) {
        // Debug.Log("WeaponManager: Enable Inventory Called");
        if(flag != ShowWeapons) { SwapShowWeapons = true; }
    }

    public void FireBow() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        Quaternion rotation = Object.transform.rotation*Quaternion.Euler(-90,0,0);
        bow.SpawnProjectile(Object.transform.position - rotation*Vector3.up*2.5F, rotation);
    }

    public void FireGreek() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        greekFire.SpawnProjectile(Object.transform.position, Object.transform.rotation);
    }
}