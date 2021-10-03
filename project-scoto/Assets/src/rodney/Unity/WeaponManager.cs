using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // FOR DEBUGGING ONLY

public class WeaponManager : MonoBehaviour {
    public int CurrentWeapon = 1;
    public bool EnableAttack = true;
    public bool BowFound = false;
    public bool TridentFound = false;
    public int FireAmount = 0;

    int timer = 0;
    int InvSize = 8;
    //int MAX_TIME = 5; // Time to switch between weaponsa

    private WeaponInputActions weapon_input_actions;
    private InputAction FireWeapon;
    private InputAction ChangeWeapon;
    
    public Bow bow;
    public GreekFire greekFire;
    public Trident trident;
    public Inv_Flashlight flashlight;

    int previous_index = 1;

    private void Awake() {
        weapon_input_actions = new WeaponInputActions();
        FireWeapon = weapon_input_actions.Player.FireWeapon;
        ChangeWeapon = weapon_input_actions.Player.ChangeWeapon;

        bow.setActive(true);
        greekFire.setActive(false);
        trident.setActive(false);                
        flashlight.setActive(false);
    }

    private void OnEnable() {
        FireWeapon.Enable();
        ChangeWeapon.Enable();
    }

    private void OnDisable() {
        FireWeapon.Disable();
        ChangeWeapon.Disable();
    }

    // Update is called once per frame
    void FixedUpdate() {
        // FIRE WEAPON
        if(FireWeapon.ReadValue<float>() != 0 && EnableAttack == true) {
            switch(CurrentWeapon) {
                case 1:
                    FireBow();
                    timer = bow.Time();
                    break;
                case 2:
                    FireGreek();
                    timer = greekFire.Time();
                    break;
            }
            EnableAttack = false;
        }
        
        if(EnableAttack == false && timer > 0) { timer --; }
        if(EnableAttack == false && timer == 0) { EnableAttack = true; }

        // CHANGE INVENTORY SLOT
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

        // INVENTORY DEBUG
        Text debug_ = GameObject.Find ("Current Inv Slot").GetComponent<Text>();
        debug_.text = CurrentWeapon.ToString();
        
        // CHANGE DISPLAYED WEAPON / ITEM
        if(WeaponChanged && EnableAttack) {
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
                    //Debug.LogError("Weapon Manager: An input was given to Which Weapon, in the inventory manager, that isnt assigned to an object. Issues with the equiped weapon may ensue.");
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
                    //Debug.LogError("Weapon Manager: An input was given to Which Weapon, in the inventory manager, that isnt assigned to an object. Issues with the equiped weapon may ensue.");
                    break;
            }
        }
    }

    public void FireBow() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        bow.SpawnProjectile(Object.transform.position, Object.transform.rotation*Quaternion.Euler(-90,0,0));
    }

    public void FireGreek() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        greekFire.SpawnProjectile(Object.transform.position, Object.transform.rotation);
    }

    // void ActivateWeapon(WeaponSuperclass this_object, bool activate_) {
    //     if(this_object.isActive() == !activate_) this_object.setActive(activate_);
    // }

    // WeaponSuperclass WhichWeapon(int CurrentWeapon) {
    //     switch(CurrentWeapon) {
    //             case 1:
    //                 return bow;
    //             case 2:
    //                 return greekFire;
    //             case 3:
    //                 return trident;
    //             // case 4:
    //             //     return flashlight;
    //             default:
    //                 Debug.LogError("Weapon Manager: An input was given to Which Weapon, in the inventory manager, that isnt assigned to an object. Issues with the equiped weapon may ensue.");
    //                 return bow;
    //     }
    // }
}