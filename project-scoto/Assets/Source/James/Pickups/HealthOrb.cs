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
    public float MinModifier = 5;
    public float MaxModifier = 12;
    Vector3 m_velocity = Vector3.zero;
    bool isFollowing = false;
    public int m_healthBonus = 2;

    protected override void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("DropLootTracker");
        m_target = m_player.transform;
        base.powerUpState = PowerUpState.InAttractMode;
        Debug.Log("HealthOrb#Start# Ive been called");
    }
    /* Function that contains payload information.
    *  Default powerup payload is overridden here
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void OnTriggerEnter (Collider collision)
    {
        Debug.Log("HealthOrb#OnTriggerEnter# Trigger enter being called");
        PowerUpCollected(collision.gameObject);
    }
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();
        PlayerData.Inst().TakeHealth(m_healthBonus);
        Debug.Log("HealthOrb#PowerUpPayLoad# Adding Health to Player");
        // playerStuff.setScoinAdjustment(addhealth);
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
            transform.position = Vector3.SmoothDamp(transform.position, m_target.position, ref m_velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}