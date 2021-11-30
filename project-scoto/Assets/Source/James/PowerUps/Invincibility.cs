/*
* Filename: Invincibility.cs
* Developer: James Lasso
* Purpose: Subclass for powerup. This gives the player invincibility
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUp
{
    /* Function that contains PowerUpPayloard override 
    *  Default powerup payload is overriden here
    *
    * Parameters: none
    *
    * Returns: none
    */   
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();
        // Set player take damage to false
        Debug.Log("Invincibility#PowerUpPayLoad: Applying Invincibility to player");
    }
}
