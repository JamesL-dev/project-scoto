using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : WeaponSuperclass {
    void Start() {
        MAX_TIME = 10; 
        weapon = GameObject.Find ("Trident");
    }
}
