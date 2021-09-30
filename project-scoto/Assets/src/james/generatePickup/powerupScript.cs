using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 m_position;
    powerupScript(Vector3 position)
    {
        // spawn at that position
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    
    }

    // Object destroyed when player collides with
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
