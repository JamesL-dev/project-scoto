using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{
    int timer = 0;

    void FixedUpdate()
    {
        timer ++;
        if (timer > 120) {Destroy(gameObject);}
    }
}
