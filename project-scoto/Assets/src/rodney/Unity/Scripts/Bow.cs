using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponSuperclass
{
    // Start is called before the first frame update
    void Start()
    {
        MAX_TIME = 10; 
        weapon = GameObject.Find ("Bow");
    }
}
