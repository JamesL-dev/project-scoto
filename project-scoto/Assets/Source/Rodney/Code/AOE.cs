/* 
 * Filename: AOE.cs
 * Developer: Rodney McCoy
 * Purpose: Control the collider and particle effects of the AOE effect that damages enemies over time
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main Class
 *
 * Member Variables:
 * m_damage -- damage per frame to enemies inside AOE effect
 * m_timer -- current lifetime of AOE effect
 * m_radius -- Radius of AOE effect
 * m_maxtime1 -- Lifetime of AOE effect before it starts shrinking
 * m_maxtime2 -- Lifetime of AOE effect with shrink time
 * m_scaleChange -- change of AOE effects scale per frame
 * m_sphere -- Sphere collider attached to object
 */
public class AOE : MonoBehaviour
{
    int m_damage = 1, m_timer = 0, m_radius = 5, m_maxTime1 = 1000, m_maxTime2 = 1100;
    Vector3 m_scaleChange = Vector3.zero; 
    // SphereCollider m_sphere = null;

    void Awake()
    {
        m_maxTime2 = m_maxTime1 - 100;
        m_scaleChange = gameObject.transform.localScale/100;
        // gameObject.GetComponent<SphereCollider>().radius = m_radius;
    }

    void FixedUpdate()
    {
        m_timer ++;
        if(m_timer >= m_maxTime2) 
        {
            gameObject.transform.localScale -= m_scaleChange;
            if(m_timer >= m_maxTime1) { Destroy(gameObject);}
        }
        if(m_timer % 10 == 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_radius);
            foreach (var hitCollider in hitColliders)
            {
                //hitCollider.SendMessage("AddDamage");
                BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hitCollider);
                if (enemy) { enemy.TakeDamage(1.5F); }
                /* REPLACE WITH OnGrenadeHit() */
            }
        }
    }

    // void OnCollisionEnter(Collision other) 
    // {
    //     //Debug.Log("1");
    //     BaseEnemy enemy = BaseEnemy.CheckIfEnemy(other.gameObject.GetComponent<Collider>());
    //     if (enemy) { enemy.TakeDamage(m_damage); /*Debug.Log("2");*/}
    // }

    /*
     * Initializes AOE effect
     * 
     * Parameters:
     * radius -- radius of AOE effect
     * maxtime -- time until AOE effect starts shrinking
     */
    // void Init(int radius, int maxTime)
    // {
    //     m_radius = radius;
    //     m_maxTime1 = maxTime;
    //     if(maxTime < 0)
    //     {
    //         Debug.LogError("Max time given to AOE.Init must be greater than or equal to zero");
    //         m_maxTime1 = 0;
    //     }
    //     m_maxTime2 = m_maxTime1 + 100;
    // }

    /*
     * Sets radius of AOE effect collider and particles
     *
     * Parameters:
     * radius -- new radius of AOE effect
     */
    // void SetRadius(int rad)
    // {
    //     if(m_sphere == null)
    //     {
    //         m_sphere = gameObject.GetComponent<SphereCollider>();
    //     }
    //     m_sphere.radius = rad;
    // }
}

