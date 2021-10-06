using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon 
{    
    void Start() 
    {
        MAX_TIME = 0; 
        discovered = true;
    }

    public override void Fire(Vector3 position, Quaternion rotation) 
    {
        rotation *= Quaternion.Euler(-90,0,0);
        Instantiate(projectile, position - rotation*Vector3.up*1.0F, rotation);
    }
}
