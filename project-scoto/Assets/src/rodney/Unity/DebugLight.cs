using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLight : MonoBehaviour
{
    int timer = 0;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if(timer == 120)
        {
            Instantiate(prefab);
        }
    }
}
