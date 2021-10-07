using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekProjectile : MonoBehaviour
{
    float velocity_scalar = 15F;
    int MAX_TIME = 45, timer = 0;
    
    public bool create_fire_at_explosion = true;
    public GameObject Explosion, Fire, Fire_small;

    void Awake() 
    { 
        GetComponent<Rigidbody>().velocity = gameObject.transform.rotation*Quaternion.Euler(80,0,0) * Vector3.up * velocity_scalar;
        Fire_small = Fire;
    }

    void FixedUpdate() 
    {
        timer ++;
        if(timer > MAX_TIME) 
        {
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation); 
            //Instantiate(Fire, gameObject.transform.position, gameObject.transform.rotation); 
            if(create_fire_at_explosion)
            {
                Instantiate(Fire, new Vector3(gameObject.transform.position.x, .2F, gameObject.transform.position.z), 
                    Quaternion.LookRotation(Vector3.right, Vector3.up)); 
                Fire_small = Instantiate(Fire_small, new Vector3(gameObject.transform.position.x, .2F, gameObject.transform.position.z), 
                    Quaternion.LookRotation(Vector3.right, Vector3.up)) as GameObject; 
                Fire_small.transform.localScale = new Vector3(1.5F, 0.75F, 1.5F);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Grenade collision occured");
    }
}
