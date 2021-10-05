using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float velocity_scalar = .2F;
    bool timer_on = true, in_ground = false, in_enemy = false;
    int MAX_TIME = 180;

    Vector3 acceleration = new Vector3(0.0F,-0.0001F,0.0F);
    Vector3 velocity = new Vector3(0,0,0);

    int timer = 0;


    void Awake() {
        velocity = -(gameObject.transform.rotation * Vector3.up * velocity_scalar);
    }

    void FixedUpdate() {
        if(!in_ground && !in_enemy) {
            gameObject.transform.position += velocity ;
            velocity += acceleration;
        }

        if(timer_on) {
            timer ++;
            if(timer > MAX_TIME) {Destroy(gameObject);}
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collision occured with layer " + other.gameObject.layer.ToString());

        if(other.gameObject.layer == LayerMask.NameToLayer("Default")) {   
            Debug.Log("Collision with default layer");
            in_enemy = true;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {   
            Debug.Log("Collision with ground layer");
            in_ground = true;
        }
    }
}
