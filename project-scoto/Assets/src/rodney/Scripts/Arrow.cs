using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float velocity = .05F;
    public bool destroy = true;
    public int MAX_TIME = 60;


    int timer = 0;

    void FixedUpdate() {
        gameObject.transform.position -= gameObject.transform.rotation * Vector3.up * velocity;
        
        if(destroy) {
            timer ++;
            if(timer > MAX_TIME) {Destroy(gameObject);}
        }
    }
}
