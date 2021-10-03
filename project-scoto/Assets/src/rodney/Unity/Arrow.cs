using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float velocity_scalar = .2F;
    bool destroy = true;
    int MAX_TIME = 180;

    Vector3 acceleration = new Vector3(0.0F,-0.0001F,0.0F);
    Vector3 velocity = new Vector3(0,0,0);

    int timer = 0;


    void Awake() {
        velocity = -(gameObject.transform.rotation * Vector3.up * velocity_scalar);
    }

    void FixedUpdate() {
        gameObject.transform.position += velocity ;
        velocity += acceleration;

        if(destroy) {
            timer ++;
            if(timer > MAX_TIME) {Destroy(gameObject);}
        }
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Arrow collision occured");
    }
}
