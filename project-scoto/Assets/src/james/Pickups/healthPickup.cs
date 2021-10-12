using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : PowerUp
{
    public int healthBonus = 50;

    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        // add health
        // playerStuff.setScoinAdjustment(addhealth);
    }

    protected override void DestroySelfAfterDelay()
    {
        Destroy(gameObject); 
    }


    // Update is called once per frame
    void Update()
    {
        // Spin scoin 
        transform.Rotate(new Vector3(1f, 0f, 0f));
    }
}