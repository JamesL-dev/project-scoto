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
    //int MAX_TIME = 5; // Time to switch between weapons

    private WeaponInputActions weapon_input_actions;
    private InputAction FireWeapon;
    private InputAction ChangeWeapon;
    
    public Bow bow;
    public GreekFire greekFire;

    private void Awake() {
        weapon_input_actions = new WeaponInputActions();
        FireWeapon = weapon_input_actions.Player.FireWeapon;
        ChangeWeapon = weapon_input_actions.Player.ChangeWeapon;
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
                    break;
                case 2:
                    FireGreek();
                    break;
            }
            EnableAttack = false;
            timer = bow.Time();
        }
        if(EnableAttack == false && timer > 0) {
            timer --;
        }
        if(EnableAttack == false && timer == 0) {
            EnableAttack = true;
        }

        // CONTROL INVENTORY
        float ChangeWeaponVal = ChangeWeapon.ReadValue<float>();
        if(ChangeWeaponVal < 0) {
            CurrentWeapon ++;
            if (CurrentWeapon > InvSize) {CurrentWeapon = 1;}
        }
        if(ChangeWeaponVal > 0) {
            CurrentWeapon --;
            if (CurrentWeapon <= 0) {CurrentWeapon = InvSize;}
        }
        Text debug_ = GameObject.Find ("Current Inv Slot").GetComponent<Text>();
        debug_.text = CurrentWeapon.ToString();
        
        if(CurrentWeapon == 1 && EnableAttack == true && bow.isActive() == false) {bow.setActive(true);}
        if(CurrentWeapon != 1 && EnableAttack == true && bow.isActive() == true) {bow.setActive(false);}

        if(CurrentWeapon == 2 && EnableAttack == true && greekFire.isActive() == false) {greekFire.setActive(true);}
        if(CurrentWeapon != 2 && EnableAttack == true && greekFire.isActive() == true) {greekFire.setActive(false);}

    }

    public void FireBow() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        bow.SpawnProjectile(Object.transform.position, Object.transform.rotation*Quaternion.Euler(-90,0,0));
    }

    public void FireGreek() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        greekFire.SpawnProjectile(Object.transform.position, Object.transform.rotation);
    }
}