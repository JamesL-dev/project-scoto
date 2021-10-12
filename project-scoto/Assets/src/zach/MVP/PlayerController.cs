using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Variables
    public float jump_force, move_speed, gravity, friction, sprint_multiplier;
    public Vector2 mouse_sens;
    public Vector2 movement_value, mouse_value;
    public float jump_value, sprinting_value;
    private Vector3 velocity;
    private float x_rotation = 0f;
    
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
        transform.Rotate(Vector3.up, mouse_value.x * mouse_sens.x);
        
        x_rotation -= mouse_value.y * mouse_sens.y;
        x_rotation = Mathf.Clamp(x_rotation, -90, 90);
        Vector3 target_rotation = transform.eulerAngles;
        target_rotation.x = x_rotation;
        player_camera.eulerAngles = target_rotation;
    }

    private void FixedUpdate() {
        move_player();
    }

    private void move_player() {
        // Apply gravity.
        velocity.y += gravity;

        // Check if player is grounded.
        bool is_grounded = Physics.CheckSphere(transform.position, 0.1f, ground_mask);
        if (is_grounded) {
            // Stop falling.
            if (velocity.y < 0) {
                velocity.y = 0;
            }

            // If jump is pressed, jump.
            if (jump_value > 0) {
                velocity.y = jump_force;
            }
        }

        // Do horizontal movement and move player.
        horizontal_movement();
        controller.Move(velocity);
    }

    private void horizontal_movement() {
        // Calculate horizontal movement relative to the player.
        Vector3 player_move = Vector3.zero;
        player_move.x = movement_value.x * move_speed;
        if (sprinting_value > 0 && movement_value.y > 0) {
            player_move.z = movement_value.y * move_speed * sprint_multiplier;
        } else {
            player_move.z = movement_value.y * move_speed;
        }

        // Convert player movement to absolute movement.
        float angle_radians = transform.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 abs_move = Vector3.zero;
        abs_move.x = (Mathf.Cos(angle_radians) * player_move.x) + (Mathf.Sin(angle_radians) * player_move.z);
        abs_move.z = (Mathf.Sin(angle_radians) * player_move.x * -1f) + (Mathf.Cos(angle_radians) * player_move.z);

        // Update horizontal velocity.
        velocity.x = (velocity.x + abs_move.x) * friction;
        velocity.z = (velocity.z + abs_move.z) * friction;
    }

    public void tp(float x, float y, float z) {
        Vector3 pos = Vector3.zero;
        pos.x = x;
        pos.y = y;
        pos.z = z;

        if (controller.enabled) {
            controller.enabled = false;
            transform.position = pos;
            controller.enabled = true;
        } else {
            transform.position = pos;
        }
    }
}
