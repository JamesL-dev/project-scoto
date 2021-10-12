using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest : MonoBehaviour
{
    int timer = 0, MAX_TIME = 60;
    void FixedUpdate()
    {
        timer ++;
        if((timer >= MAX_TIME || gameObject.transform.position.y <= 0) && gameObject.transform.parent == null)
        {
            RodneyStressTest.TestSucceeded = true;
            //Debug.LogError("Stress Test Succedded. An arrow didn't connect with the heavy enemy. Arrow position is " + gameObject.transform.position.ToString());
        }
    }
}
