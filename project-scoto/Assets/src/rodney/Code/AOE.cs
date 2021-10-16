using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    int DAMAGE = 1, timer = 0, MAX_TIME = 1000, radius = 5;

    Vector3 scale_change = Vector3.zero; 
    SphereCollider sphere = null;
    int MAX_TIME_0;

    void Awake()
    {
        MAX_TIME_0 = MAX_TIME - 100;
        scale_change = gameObject.transform.localScale/100;
        gameObject.GetComponent<SphereCollider>().radius = radius;
    }

    void FixedUpdate()
    {
        timer ++;
        if(timer >= MAX_TIME_0) 
        {
            gameObject.transform.localScale -= scale_change;
            if(timer >= MAX_TIME) { Destroy(gameObject);}
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        //Debug.Log("1");
        BaseEnemy enemy = BaseEnemy.CheckIfEnemy(other.gameObject.GetComponent<Collider>());
        if (enemy) { enemy.TakeDamage(DAMAGE); /*Debug.Log("2");*/}
    }

    void Init(int _radius, int _MAX_TIME)
    {
        radius = _radius;
        if(MAX_TIME < 100)
        {
            Debug.LogError("Max time given to AOE.Init must be 100 or greater");
            MAX_TIME = 100;
        }
        MAX_TIME = _MAX_TIME;
        MAX_TIME_0 = MAX_TIME - 100;
    }

    void SetRadius(int rad)
    {
        if(sphere == null)
        {
            sphere = gameObject.GetComponent<SphereCollider>();
        }
        sphere.radius = rad;
    }
}
