/*
 * Filename: PlayerData.cs
 * Developer: Zachariah Preston
 * Purpose: Allows for storage and modification of various player values. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Singleton that allows for storage and modification of various player values.
 *
 * Member variables:
 * m_health -- Float for player's health.
 * m_maxHealth -- Float for the maximum value of the player's health
 * m_instance -- Static intance of itself for the Singleton pattern.
 */
public sealed class PlayerData : MonoBehaviour
{
    ////////////////////////////// Variables //////////////////////////////
    public float m_health, m_maxHealth;
    private static PlayerData m_instance;

    /* Gets a reference to the instance of the singleton, creating the instance if necessary.
     *
     * Returns:
     * PlayerData -- Reference to the player data.
     */
    public static PlayerData Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("Player").GetComponent<PlayerData>();
        }
        return m_instance;
    }

    ////////////////////////////// Setters //////////////////////////////
    /*
    * Gives the enemy a specified amount of health
    *
    * Parameters:
    * health - amount of health to give
    */
    public void TakeHealth(float health)
    {
        TakeDamage(-health);
    }

    /*
    * Takes damage away from the player
    *
    * Parameters:
    * damage - amount of damage to take
    */
    public void TakeDamage(float damage)
    {
        m_health -= damage;
    
        if (m_health <= 0)
        {
            m_health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (m_health >= m_maxHealth)
        {
            m_health = m_maxHealth;
        }
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
    /*
    * Gets the current health of the player
    *
    * Returns:
    * float - health of the player
    */
    public float GetHealth() {return m_health;}

    /*
    * Gets the current health of the player as a percent
    *
    * Returns:
    * float - gets the current health percent. Should return value [0, 1]
    */
    public float GetHealthPercent() {return m_health/m_maxHealth;}

    /*
    * Gets the max health the player can have
    *
    * Returns:
    * float - returns m_maxHealth (the maximum health the player can have)
    */
    public float GetMaxHealth() {return m_maxHealth;}


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

    /* Makes the singleton's constructor static.
     */
    private PlayerData() {}
}

