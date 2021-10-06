using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldCoin : PowerUp
{

    [Header("Coin Random Splash")]
    public Transform objTrans;
    private float delay  = 0;
    private float pasttime = 0;
    private float when = 1.0f;
    private Vector3 off;

    private void Awake()
    {
        //Random x axis
        off = new Vector3(Random.Range(-3, 3), off.y, off.z);
        //random y axis
        off = new Vector3(Random.Range(-3, 3), off.z);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (when >= delay)
        {
            objTrans.position += off * Time.deltaTime;
            delay += pasttime;
        }
        
    }
}
