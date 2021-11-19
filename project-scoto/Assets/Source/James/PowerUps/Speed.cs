using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : PowerUp
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
        // Set new player speed
        Debug.Log("Speed#PowerUpPayload: Applying speed increase to player");
    }
}
