/*
 * Filename: PlayerData.cs
 * Developer: Zachariah Preston
 * Purpose: Allows for storage and modification of various player values. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Allows for storage and modification of various player values.
 *
 * Member variables:
 * m_health -- Float for player's health.
 * m_battery -- Float for player's battery.
 */
public class PlayerData : MonoBehaviour
{
    ////////////////////////////// Variables //////////////////////////////
    public float m_health, m_battery;

    ////////////////////////////// Setters //////////////////////////////
    /* Set the player's health.
     *
     * Parameters:
     * h - Float for health.
     */
    public void SetHealth(float h)
    {
        m_health = h;
    }

    /* Set the player's battery.
     *
     * Parameters:
     * b - Float for battery.
     */
    public void SetBattery(float b)
    {
        m_battery = b;
    }

    /* Set the player's jump force.
     *
     * Parameters:
     * jf - Float for jump force.
     */
    public void SetJumpForce(float jf)
    {
        GetComponent<PlayerController>().m_jumpForce = jf;
    }

    /* Set the player's movement speed.
     *
     * Parameters:
     * ms - Float for movement speed.
     */
    public void SetMoveSpeed(float ms)
    {
        GetComponent<PlayerController>().m_moveSpeed = ms;
    }

    /* Set the player's gravity.
     *
     * Parameters:
     * h - Float for gravity.
     */
    public void SetGravity(float g)
    {
        GetComponent<PlayerController>().m_gravity = g;
    }

    /* Set the player's friction.
     *
     * Parameters:
     * f - Float for friction.
     */
    public void SetFriction(float f)
    {
        GetComponent<PlayerController>().m_friction = f;
    }

    /* Set the player's sprint multiplier.
     *
     * Parameters:
     * sm - Float for sprint multiplier.
     */
    public void SetSprintMultiplier(float sm)
    {
        GetComponent<PlayerController>().m_sprintMultiplier = sm;
    }

    ////////////////////////////// Getters //////////////////////////////
    /* Get the player's health.
     *
     * Returns:
     * float -- Health.
     */
    public float GetHealth()
    {
        return m_health;
    }

    /* Get the player's battery.
     *
     * Returns:
     * float -- Battery.
     */
    public float GetBattery()
    {
        return m_battery;
    }

    /* Get the player's jump force.
     *
     * Returns:
     * float -- Jump force.
     */
    public float GetJumpForce()
    {
        return GetComponent<PlayerController>().m_jumpForce;
    }

    /* Get the player's movement speed.
     *
     * Returns:
     * float -- Movement speed.
     */
    public float GetMoveSpeed()
    {
        return GetComponent<PlayerController>().m_moveSpeed;
    }

    /* Get the player's gravity.
     *
     * Returns:
     * float -- Gravity.
     */
    public float GetGravity()
    {
        return GetComponent<PlayerController>().m_gravity;
    }

    /* Get the player's friction.
     *
     * Returns:
     * float -- Friction.
     */
    public float GetFriction()
    {
        return GetComponent<PlayerController>().m_friction;
    }

    /* Get the player's sprint multiplier.
     *
     * Returns:
     * float -- Sprint multiplier.
     */
    public float GetSprintMultiplier()
    {
        return GetComponent<PlayerController>().m_sprintMultiplier;
    }
}

