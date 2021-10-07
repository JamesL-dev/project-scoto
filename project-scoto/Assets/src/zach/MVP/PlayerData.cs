using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    // Variables
    public float health, battery;

    // Setters
    public void set_health(float h) {
        health = h;
    }

    public void set_battery(float b) {
        battery = b;
    }

    public void set_jump_force(float jf) {
        GetComponent<PlayerController>().jump_force = jf;
    }

    public void set_move_speed(float ms) {
        GetComponent<PlayerController>().move_speed = ms;
    }

    public void set_gravity(float g) {
        GetComponent<PlayerController>().gravity = g;
    }

    public void set_friction(float f) {
        GetComponent<PlayerController>().friction = f;
    }

    public void set_sprint_multiplier(float sm) {
        GetComponent<PlayerController>().sprint_multiplier = sm;
    }

    // Getters
    public float get_health() {
        return health;
    }

    public float get_battery() {
        return battery;
    }

    public float get_jump_force() {
        return GetComponent<PlayerController>().jump_force;
    }

    public float get_move_speed() {
        return GetComponent<PlayerController>().move_speed;
    }

    public float get_gravity() {
        return GetComponent<PlayerController>().gravity;
    }

    public float get_friction() {
        return GetComponent<PlayerController>().friction;
    }

    public float get_sprint_multiplier() {
        return GetComponent<PlayerController>().sprint_multiplier;
    }
}
