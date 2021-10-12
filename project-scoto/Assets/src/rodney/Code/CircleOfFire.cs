using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOfFire : MonoBehaviour
{
    public int TIME1 = 60;
    int timer = 0, TIME2 = 0;

    Vector3 scale_change; 

    void Awake()
    {
        TIME2 = TIME1 + 100;
        scale_change = gameObject.transform.localScale/100;
    }
    void FixedUpdate()
    {
        timer ++;
        if(timer >= TIME1) 
        {
            gameObject.transform.localScale -= scale_change;
            if(timer >= TIME2) { Destroy(gameObject);}
        }
    }
}
