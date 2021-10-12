using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanController : MonoBehaviour
{
    int timer = 0;

    void FixedUpdate()
    {
        timer ++;
        if (timer > 60) {Destroy(gameObject);}
    }
}
