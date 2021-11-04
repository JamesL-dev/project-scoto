/*
* Filename: EnergyOrb.cs
* Developer: James Lasso
* Purpose: Health pickup contains payload information and other overrides.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Energy powerup.
* Contains payload and overrides.
*
* Member variables: none
*/
public class EnergyOrb : PowerUp
{
    public int m_EnergyBonus;
    GameObject m_player;
    public Transform m_target;
    public float MinModifier = 5;
    public float MaxModifier = 12;
    Vector3 m_velocity = Vector3.zero;
    bool isFollowing = false;

    void start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_target = m_player.transform;
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

        // add energy
        // playerStuff.setScoinAdjustment(addEnergy);
    }

    /* Function to destroy object when picked up
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void DestroySelfAfterDelay()
    {
        Destroy(gameObject); 
    }

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
