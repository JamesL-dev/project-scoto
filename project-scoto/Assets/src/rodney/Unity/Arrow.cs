using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float velocity_scalar = 4F, acceleration_scalar = 1F, damage = 10F;
    [SerializeField] int MAX_TIME = 400, timer = 0;

    bool faster_timer = true, in_ground = false, in_enemy = false, fade_light = false;

    Vector3 acceleration = new Vector3(0.0F,-0.001F,0.0F);
    Vector3 velocity = new Vector3(0,0,0);

    Light light_;

    void Awake() 
    {
        velocity = -(gameObject.transform.rotation * Vector3.up * velocity_scalar);
        acceleration *= acceleration_scalar;
        light_ = gameObject.AddComponent<Light>();
        light_.color = Color.white;
        light_.range = 20;
        light_.intensity = .25F;
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
        if(fade_light) 
        {
            light_.intensity -= .003F;
            if(light_.intensity == 0) {fade_light = false;}
        }
        if(timer > MAX_TIME) {Destroy(gameObject);}
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {   
            if(!in_ground) 
            {
                in_enemy = faster_timer = true; 
                fade_light = true;
            }
            gameObject.transform.parent = other.transform;
            if(other.gameObject.name == "HeavyEnemy")
            {
                // DO WORK
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {   
            Debug.Log("Collision with ground layer");
            if(!in_enemy) 
            {   
                in_ground = faster_timer = true;
                fade_light = true;
            }
        }
    }
}
