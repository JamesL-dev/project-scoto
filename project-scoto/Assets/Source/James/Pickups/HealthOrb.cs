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
    GameObject m_player;
    public Transform m_target;
    public float m_minModifier = 5;
    public float m_maxModifier = 12;
    public int m_minbonus = 1;
    public int m_maxBonus = 100;
    Vector3 m_velocity = Vector3.zero;
    bool isFollowing = false;
    public int m_healthBonus = 2;

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
    * Parameters: none
    *
    * Returns: none
    */
    public void setHealthBonus(int bonus)
    {
        if (bonus < m_minbonus)
        {
            m_healthBonus = m_minbonus;
        }
        else if (bonus > m_maxBonus)
        {
            m_healthBonus = m_maxBonus;
        }
        else
        {
            m_healthBonus = bonus;
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