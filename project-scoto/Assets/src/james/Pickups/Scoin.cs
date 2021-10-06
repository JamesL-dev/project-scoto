using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoin : PowerUp
{
    public int scoinBonus = 1;

    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        // Add 1 scoin to the players scoin count
        // playerStuff.setScoinAdjustment(addScoin);
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
