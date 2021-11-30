/*
* Filename: HealthOrb.cs
* Developer: James Lasso
* Purpose: Health pickup contains payload information and other overrides.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Health powerup.
* Contains payload and overrides.
*
* Member variables: none
*/
public class HealthOrb : PowerUp
{
    GameObject m_player; // Reference to player
    public Transform m_target; // Target for follow script
    public int m_healthBonus = 2; // THE health bonus value
    public float m_minModifier = 5; // Used for follow target
    public float m_maxModifier = 12; // Used for follow target
    public int m_minbonus = 1; // Minimum health bonus amount
    public int m_maxBonus = 100; // Maximum health bonus amount
    Vector3 m_velocity = Vector3.zero;
    bool isFollowing = false; // Used for follow script

    /* Function that contains Start override 
    *  Default powerup Start is overridden here
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("DropLootTracker");
        m_target = m_player.transform;
        base.Start();
    }
    /* Function that contains onTriggerEnter override 
    *  Default powerup onTriggerEnter is overridden here
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void OnTriggerEnter (Collider collision)
    {
        PowerUpCollected(collision.gameObject);
    }
    /* Function that contains payload information.
    *  Default powerup payload is overridden here
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();
        PlayerData.Inst().TakeHealth(m_healthBonus);
        Debug.Log("HealthOrb#PowerUpPayload: Adding " + m_healthBonus + " to player health");
    }

    /* Function to destroy object when picked up
    *
    * Parameters: none
    *
    * Returns: none
    */
    public void StartFollowing()
    {
        isFollowing = true;
    }

    /* Function that sets health bonus
    *  
    *
    * Parameters: int amount,
    *
    * Returns: none
    */
    public void setHealthBonus(int m_amount)
    {
        if (m_amount < m_minbonus)
        {
            m_healthBonus = m_minbonus;
        }
        else if (m_amount > m_maxBonus)
        {
            m_healthBonus = m_maxBonus;
        }
        else
        {
            m_healthBonus = m_amount;
        }
    }

    /* Function that updates every frame.
    *  In this case its used to animate the object.
    *
    * Parameters: none
    *
    * Returns: none
    */
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_target.position, ref m_velocity, Time.deltaTime * Random.Range(m_minModifier, m_maxModifier));
        }
    }
}