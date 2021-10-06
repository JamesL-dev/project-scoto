using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekProjectile : MonoBehaviour
{
    [SerializeField] float velocity_scalar = .4F;
    public bool destroy = true;
    int MAX_TIME = 45;

    Vector3 acceleration = new Vector3(0.0F,-0.001F,0.0F);
    Vector3 velocity = new Vector3(0,0,0);

    public GameObject Explosion, Fire;

    int timer = 0;

    void Awake() { velocity = gameObject.transform.rotation*Quaternion.Euler(80,0,0) * Vector3.up * velocity_scalar; }

    void FixedUpdate() 
    {
        gameObject.transform.position += velocity ;
        velocity += acceleration;
        
        if(destroy) 
        {
            timer ++;
            if(timer > MAX_TIME) 
            {
                Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation); 
                Instantiate(Fire, gameObject.transform.position, gameObject.transform.rotation); 
                
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Grenade collision occured");
    }
}
