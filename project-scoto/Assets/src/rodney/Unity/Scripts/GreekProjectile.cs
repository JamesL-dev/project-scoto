using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekProjectile : MonoBehaviour
{
    public float velocity = 1F;
    public bool destroy = true;
    public int MAX_TIME = 90;


    int timer = 0;

    void FixedUpdate() {
        gameObject.transform.position += gameObject.transform.rotation*Quaternion.Euler(90,0,0) * Vector3.up * velocity;
        
        if(destroy) {
            timer ++;
            if(timer > MAX_TIME) {Destroy(gameObject);}
        }
    }
}
