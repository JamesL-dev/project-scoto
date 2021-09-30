using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour {
    public int CurrentWeapon = 0;
    public bool EnableAttack = true;
    public bool BowFound = false;
    public bool TridentFound = false;
    public int FireAmount = 0;

    public int timer = 0;
    public int TIMER_MAX = 100;

    private WeaponInputActions weapon_input_actions;
    private InputAction FireWeapon;

    //public Bow bow;
    
    private void Awake() {
        weapon_input_actions = new WeaponInputActions();
        FireWeapon = weapon_input_actions.Player.FireWeapon;
    }

    private void OnEnable() {
        FireWeapon.Enable();
    }

    private void OnDisable() {
        FireWeapon.Disable();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(FireWeapon.ReadValue<float>() != 0 && EnableAttack == true) {
            FireBow();
            EnableAttack = false;
            timer = TIMER_MAX;
        }
        if(EnableAttack == false && timer > 0) {
            timer --;
        }
        if(EnableAttack == false && timer == 0) {
            EnableAttack = true;
        }
    }

    public void FireBow() {
        GameObject Object = GameObject.FindGameObjectWithTag("MainCamera");
        Debug.Log("Fired Bow");
        GameObject.Find ("Bow").GetComponent<WeaponSuperclass>().SpawnProjectile(Object.transform.position, Object.transform.rotation*Quaternion.Euler(-90,0,0));
    }
}
