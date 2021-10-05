using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv_Flashlight : Weapon
{
    void Start()
    {
        MAX_TIME = 15;
    }

    public override void Fire(Vector3 position, Quaternion rotation) { Instantiate(projectile, position + rotation * new Vector3(0,-1,1), rotation); }
}