/*
* Filename: Scoin.cs
* Developer: James Lasso
* Purpose: Scoin pickup contains payload information and overrides
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Scoin powerup
* Contains payload and overrides.
*
* Member variables: none
*/
public class Scoin : PowerUp
{
    public int scoinBonus = 1;

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

        // Add 1 scoin to the players scoin count
        // playerStuff.setScoinAdjustment(addScoin);
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
        if (scoinBonus > 5)
        {
            scoinBonus = 5;
        }
        if (scoinBonus < 1)
        {
            scoinBonus = 1;
        }
    }
}
