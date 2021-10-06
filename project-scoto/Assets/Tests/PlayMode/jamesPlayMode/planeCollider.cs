using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeCollider : MonoBehaviour
{
    public bool isPlaneTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "testSphere")
        {
            isPlaneTouched = true;
        }
    }
}
