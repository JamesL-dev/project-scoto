using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Variables
    public float jump_force, move_speed, gravity, friction, sprint_multiplier;
    public Vector2 mouse_sens;
    private Vector3 player_vel, velocity; // player_vel is relative to the player, velocity is absolute
    private bool is_grounded;
    private float x_rotation = 0f;
    private Vector2 movement_value, mouse_value;
    private float jump_value, sprinting_value;
    
    // Classes
    public LayerMask ground_mask;
    public Transform player_camera;
    private PlayerInputActions player_input_actions;
    private InputAction movement, jumping, mouse, sprinting;
    private CharacterController controller;

    private void Awake() {
        player_input_actions = new PlayerInputActions();
        movement = player_input_actions.Player.Movement;
        jumping = player_input_actions.Player.Jumping;
        mouse = player_input_actions.Player.Mouse;
        sprinting = player_input_actions.Player.Sprinting;

        controller = GetComponent<CharacterController>();
    }

    private void OnEnable() {
        movement.Enable();
        jumping.Enable();
        mouse.Enable();
        sprinting.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable() {
        movement.Disable();
        jumping.Disable();
        mouse.Disable();
        sprinting.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update() {
        // Get inputs.
        movement_value = movement.ReadValue<Vector2>();
        jump_value = jumping.ReadValue<float>();
        mouse_value = mouse.ReadValue<Vector2>();
        sprinting_value = sprinting.ReadValue<float>();

        // Rotate from mouse.
        transform.Rotate(Vector3.up, mouse_value.x * mouse_sens.x * Time.deltaTime);
        
        x_rotation -= mouse_value.y * mouse_sens.y;
        x_rotation = Mathf.Clamp(x_rotation, -90, 90);
        Vector3 target_rotation = transform.eulerAngles;
        target_rotation.x = x_rotation;
        player_camera.eulerAngles = target_rotation;
    }

    private void FixedUpdate() {
        // Apply gravity.
        player_vel.y += gravity;

        // Check if player is grounded.
        is_grounded = Physics.CheckSphere(transform.position, 0.1f, ground_mask);
        if (is_grounded) {
            // Update player's relative velocity.
            player_vel.x += movement_value.x * move_speed;
            player_vel.x *= friction;
            if (sprinting_value > 0 && player_vel.z > 0) {
                player_vel.z += movement_value.y * move_speed * sprint_multiplier;
            } else {
                player_vel.z += movement_value.y * move_speed;
            }
            player_vel.z *= friction;
            
            // Stop falling.
            if (player_vel.y < 0) {
                player_vel.y = 0f;

            }

            // Jump.
            if (jump_value > 0) {
                player_vel.y = jump_force;
            }
        }

        // Update velocity and move player.
        float angle_radians = transform.eulerAngles.y * Mathf.Deg2Rad;
        velocity.x = (Mathf.Cos(angle_radians) * player_vel.x) + (Mathf.Sin(angle_radians) * player_vel.z);
        velocity.y = player_vel.y;
        velocity.z = (Mathf.Sin(angle_radians) * player_vel.x * -1f) + (Mathf.Cos(angle_radians) * player_vel.z);
        controller.Move(velocity);
    }
}
