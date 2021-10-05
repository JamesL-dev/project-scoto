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

    void FixedUpdate()
    {
        if(timer > 0) {timer --;}

        if(DEBUGGING.ReadValue<float>() != 0 && timer == 0) {
            /* This line is what allows you to turn the inventory on and off. The first part gives access to the function EnableInventory.
            The second part, EnableInventory(bool var), turns the inventory ON if var is true (and if the inventory is currently off) and it turns the
            inventory OFF if var is false (and the inventory is currently on).
            Feel free to use this line in code if needed but i might be changing it to be more simple, by using a namespace,
            so be wary of if i change, which i will notify discord if i do */
            GameObject.Find("Inventory").GetComponent<WeaponManager>().EnableInventory(!curr_state);
            
            curr_state = !curr_state; // Keep track of current state of inventory
            timer = 60; // Only toggle inventory every 60 frames
        }
    }

    private void OnEnable() {
        DEBUGGING.Enable();
    }

    private void OnDisable() {
        DEBUGGING.Disable();
    }
}
