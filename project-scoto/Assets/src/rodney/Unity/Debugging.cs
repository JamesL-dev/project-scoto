using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Debugging : MonoBehaviour
{
    public int timer = 0;
    public bool curr_state = true;
    private WeaponInputActions weapon_input_actions;
    private InputAction DEBUGGING;

    void Awake(){ 
        weapon_input_actions = new WeaponInputActions(); //
        DEBUGGING = weapon_input_actions.Player.DEBUGGING;
    }

    void Update()
    {
        if(timer != 0) {timer --;}
        if(DEBUGGING.ReadValue<float>() != 0 && timer == 0) {
            // Debug.Log("Debugging: " + curr_state.ToString());
            GameObject.Find("Inventory").GetComponent<WeaponManager>().EnableInventory(!curr_state);
            curr_state = !curr_state;
            timer = 120;
        }
    }

    private void OnEnable() {
        DEBUGGING.Enable();
    }

    private void OnDisable() {
        DEBUGGING.Disable();
    }
}
