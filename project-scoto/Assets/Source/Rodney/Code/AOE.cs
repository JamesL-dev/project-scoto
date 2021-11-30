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
 */
public class AOE : MonoBehaviour
{
    [SerializeField] public float m_damage = 1F;

    int m_timer = 0, m_radius = 5, m_maxTime1 = 300, m_maxTime2 = 350;
    Vector3 m_scaleChange = Vector3.zero; 

    void Awake()
    {
        m_maxTime2 = m_maxTime1 + 100;
        m_scaleChange = gameObject.transform.localScale/100;
    }

    void FixedUpdate()
    {
        m_timer ++;
        // After m_maxTime1 frames, start shrinking ring of fire. After m_maxTime2, delete instance
        if(m_timer >= m_maxTime1) 
        {
            gameObject.transform.localScale -= m_scaleChange;
            if(m_timer >= m_maxTime2) { Destroy(gameObject);}
        }
        // 12 times per second, damage enemies if they are in range
        if(m_timer % 5 == 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_radius);
            foreach (var hitCollider in hitColliders)
            {
                BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hitCollider);
                if (enemy) { enemy.HitEnemy(BaseEnemy.WeaponType.AOE, m_damage); }
            }
        }
    }
}

