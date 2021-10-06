using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float velocity_scalar = 4F, acceleration_scalar = 1F, damage = 10F;
    [SerializeField] int MAX_TIME = 10000, timer = 0;

    bool faster_timer = true, in_ground = false, in_enemy = false;

    Vector3 acceleration = new Vector3(0.0F,-0.001F,0.0F);
    Vector3 velocity = new Vector3(0,0,0);

    void Awake() 
    {
        velocity = -(gameObject.transform.rotation * Vector3.up * velocity_scalar);
        acceleration *= acceleration_scalar;
        Light lightComp = gameObject.AddComponent<Light>();
        lightComp.color = Color.white;
        lightComp.range = 20;
        lightComp.intensity = .25F;
    }

    void FixedUpdate() 
    {
        if(!in_ground && !in_enemy) 
        {
            gameObject.transform.position += velocity ;
            velocity += acceleration;
        }

        timer ++;
        if(faster_timer) { timer ++; }
        if(timer > MAX_TIME) {Destroy(gameObject);}
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer != 7) { Debug.Log("Collision occured with layer " + other.gameObject.layer.ToString());}

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {   
            if(!in_ground) {in_enemy = faster_timer = true; }
            gameObject.transform.parent = other.transform;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {   
            Debug.Log("Collision with ground layer");
            if(!in_enemy) {in_ground = faster_timer = true;}
        }
    }
}
