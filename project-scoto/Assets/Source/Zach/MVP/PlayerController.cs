/*
 * Filename: PlayerController.cs
 * Developer: Zachariah Preston
 * Purpose: Controls the player's movement and camera by using the new Unity Input System.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * Controls the player's movement and camera by using the new Unity Input System.
 *
 * Member variables:
 * m_jumpForce -- Float for the velocity applied to the player when jumping.
 * m_moveSpeed -- Float for the player's movement speed.
 * m_gravity -- Float for the player's gravity.
 * m_friction -- Float for the player's friction.
 * m_sprintMultiplier -- Float for the amount to multiply the player's forward speed when sprinting.
 * m_mouseSens -- Vector2 for the player's mouse look sensitivity.
 * m_groundMask -- LayerMask for detecting when the player is standing on the ground.
 * m_playerCamera -- Transform for the player camera's position and rotation.
 * m_movementValue -- Vector2 for the player's x and z movement inputs.
 * m_mouseValue -- Vector2 for the player's x and y mouse inputs.
 * m_jumpValue -- Float for the player's jump input (0.0f is false, 1.0f is true).
 * m_sprintingValue -- Float for the player's sprinting input (0.0f is false, 1.0f is true).
 * m_velocity -- Vector3 for the player's current x, y, and z movement velocities.
 * m_xRotation -- Float for the player camera's x rotation.
 * m_playerInputActions -- PlayerInputActions (input actions superclass) for the inputs.
 * m_movement -- InputAction for movement.
 * m_jumping -- InputAction for jumping.
 * m_mouse -- InputAction for camera rotation.
 * m_sprinting -- InputAction for sprinting.
 * m_controller -- CharacterController for moving the player and detecting collisions.
 */
public class PlayerController : MonoBehaviour
{
    public float m_jumpForce, m_moveSpeed, m_gravity, m_friction, m_sprintMultiplier;
    public Vector2 m_mouseSens;
    public LayerMask m_groundMask;
    public Transform m_playerCamera;

    private Vector2 m_movementValue, m_mouseValue;
    private float m_jumpValue, m_sprintingValue;
    private Vector3 m_velocity;
    private float m_xRotation = 0f;
    private PlayerInputActions m_playerInputActions;
    private InputAction m_movement, m_jumping, m_mouse, m_sprinting;
    private CharacterController m_controller;
    
    /* Sets up the input actions.
     */
    private void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
        m_movement = m_playerInputActions.Player.Movement;
        m_jumping = m_playerInputActions.Player.Jumping;
        m_mouse = m_playerInputActions.Player.Mouse;
        m_sprinting = m_playerInputActions.Player.Sprinting;

        m_controller = GetComponent<CharacterController>();
    }

    /* Enambles the input actions when the player is enabled.
     */
    private void OnEnable()
    {
        m_movement.Enable();
        m_jumping.Enable();
        m_mouse.Enable();
        m_sprinting.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    /* Disables the input actions when the player is disabled.
     */
    private void OnDisable()
    {
        m_movement.Disable();
        m_jumping.Disable();
        m_mouse.Disable();
        m_sprinting.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    /* Gets the inputs for the player (either from the input actions or the demo) and rotates the player camera.
     */
    private void Update()
    {
        // Get inputs from input actions.
        m_movementValue = m_movement.ReadValue<Vector2>();
        m_jumpValue = m_jumping.ReadValue<float>();
        m_mouseValue = m_mouse.ReadValue<Vector2>();
        m_sprintingValue = m_sprinting.ReadValue<float>();

        // Check for any movement/mouse inputs.
        if (m_movementValue.x != 0f || m_movementValue.y != 0f || m_jumpValue != 0f ||
            m_mouseValue.x != 0f || m_mouseValue.y != 0f || m_sprintingValue != 0f)
        {
            Demo.ResetTimer();
        }

        // If demo mode is on, replace inputs with inputs from demo.
        if (Demo.On())
        {
            m_movementValue = Demo.Move();
            m_jumpValue = Demo.Jump();
            m_mouseValue = Demo.Mouse();
            m_sprintingValue = Demo.Sprint();
        }

        // Rotate from mouse.
        transform.Rotate(Vector3.up, m_mouseValue.x * m_mouseSens.x);
        
        m_xRotation -= m_mouseValue.y * m_mouseSens.y;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);
        Vector3 target_rotation = transform.eulerAngles;
        target_rotation.x = m_xRotation;
        m_playerCamera.eulerAngles = target_rotation;
    }

    /* Moves the player.
     */
    private void FixedUpdate()
    {
        MovePlayer();
    }

    /* Teleports the player to the given position.
     *
     * Parameters:
     * pos -- Vector3 for the position to go to.
     */
    public void Tp(Vector3 pos)
    {
        if (m_controller.enabled)
        {
            m_controller.enabled = false;
            transform.position = pos;
            m_controller.enabled = true;
        }
        else
        {
            transform.position = pos;
        }
    }

    /* Moves the player.
     */
    private void MovePlayer()
    {
        // Apply m_gravity.
        m_velocity.y += m_gravity;

        // Check if player is grounded.
        bool is_grounded = Physics.CheckSphere(transform.position, 0.1f, m_groundMask);
        if (is_grounded)
        {
            // Stop falling.
            if (m_velocity.y < 0)
            {
                m_velocity.y = 0;
            }

            // If jump is pressed, jump.
            if (m_jumpValue > 0)
            {
                m_velocity.y = m_jumpForce;
            }
        }

        // Do horizontal movement and move player.
        HorizontalMovement();
        m_controller.Move(m_velocity);
    }

    /* Controls the player's horizontal movement.
     */
    private void HorizontalMovement()
    {
        // Calculate horizontal movement relative to the player.
        Vector3 player_move = Vector3.zero;
        player_move.x = m_movementValue.x * m_moveSpeed;
        if (m_sprintingValue > 0 && m_movementValue.y > 0)
        {
            player_move.z = m_movementValue.y * m_moveSpeed * m_sprintMultiplier;
        }
        else
        {
            player_move.z = m_movementValue.y * m_moveSpeed;
        }

        // Convert player movement to absolute movement.
        float angle_radians = transform.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 abs_move = Vector3.zero;
        abs_move.x = (Mathf.Cos(angle_radians) * player_move.x) + (Mathf.Sin(angle_radians) * player_move.z);
        abs_move.z = (Mathf.Sin(angle_radians) * player_move.x * -1f) + (Mathf.Cos(angle_radians) * player_move.z);

        // Update horizontal m_velocity.
        m_velocity.x = (m_velocity.x + abs_move.x) * m_friction;
        m_velocity.z = (m_velocity.z + abs_move.z) * m_friction;
    }
}

