/*
* Filename: HealthPickup.cs
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
public class HealthPickup : PowerUp
{
    public int m_healthBonus = 50;

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

        // add health
        // playerStuff.setScoinAdjustment(addhealth);
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

    /* Function that updates every frame.
    *  In this case its used to animate the object.
    *
    * Parameters: none
    *
    * Returns: none
    */
    void Update()
    {
        transform.Rotate(new Vector3(1f, 0f, 0f)); // Spin
    }
}