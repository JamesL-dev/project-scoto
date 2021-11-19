/*
* Filename: Health.cs
* Developer: James Lasso
* Purpose: Subclass of PowerUp this is for a permenant health increase
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PowerUp
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
        // Set new max player health
        Debug.Log("Health#PowerUpPayLoad: Permanently Increasing player health");
    }
}
