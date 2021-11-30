/*
* Filename: InfiniteAmmo.cs
* Developer: James Lasso
* Purpose: Subclass of PowerUp. This is gives the player infinite ammo
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmo : PowerUp
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
        // Set infinite ammo temporarily
        Debug.Log("InfiniteAmmo#PowerUpPayLoad: Applying infinite ammo to player");
    }
}
