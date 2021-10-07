using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] public float velocity_scalar = 4F, acceleration_scalar = 1F, damage = 10F, MAX_TIME = 60F;
    int timer = 0;

    bool in_air = true;

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
        if(in_air) 
        {
            gameObject.transform.position += velocity ;
            velocity += acceleration;
        }

        timer ++;
        if(!in_air) 
        {
            timer ++;
            light_.intensity -= .003F;
            //if(light_.intensity == 0) {fade_light = false;}
        }
        if(timer > MAX_TIME) {Destroy(gameObject);}
    }

    void OnTriggerEnter(Collider other) 
    {
        if(in_air)
        {
            bool ignore = false;

            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            {   
                gameObject.transform.parent = other.transform;
                if(other.gameObject.name == "HeavyEnemy") { other.gameObject.GetComponent<HeavyEnemy>().TakeDamage(damage); }
            } 
            else if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) 
            {   
            } 
            else
            {
                ignore = true;
            }

            if(!ignore)
            {
                in_air = false;
                gameObject.transform.position += .5F*velocity ;
            }
        }
    }
}
