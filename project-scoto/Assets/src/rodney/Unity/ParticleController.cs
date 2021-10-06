using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float scale_change = .999F;
    public int MAX_TIME = 150;
    int timer = 0;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale *= scale_change;
        timer ++;
        if(timer >= MAX_TIME) {Destroy(gameObject);}
    }
}
