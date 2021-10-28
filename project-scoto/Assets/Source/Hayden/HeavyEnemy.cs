/*
 * Filename: HeavyEnemy.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the HeavyEnemy subclass.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * HeavyEnemy is a subclass of abstract BaseEnemy.
 * This class contains implementation on top of BaseEnemy to create a concrete
 * type.
 */
public class HeavyEnemy : BaseEnemy
{
    protected override void Initialize()
    {
        m_maxHealth = 300.0f;
        m_walkSpeed = 1.0f;
        m_runSpeed = 4.0f;
        m_walkPointWait = 5.0f;
        m_damagePerHit = 20.0f;
        m_attackRange = 2.0f;
        m_sightRange = 10.0f;
        m_attackWait = 2.0f;


        Vector3 roomSize = m_roomIn.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_walkPointRange = Mathf.Min(roomSize.x, roomSize.z) * 0.5f * 0.75f;

        m_walkSource = gameObject.AddComponent<AudioSource>();
        m_idleSource = gameObject.AddComponent<AudioSource>();
        m_attackSource = gameObject.AddComponent<AudioSource>();
        m_runSource = gameObject.AddComponent<AudioSource>();
        m_dieSource = gameObject.AddComponent<AudioSource>();

        m_walkSource.clip = m_walkSourceClip;
        m_idleSource.clip = m_idleSourceClip;
        m_attackSource.clip = m_attackSourceClip;
        m_dieSource.clip = m_dieSourceClip;
        m_runSource.clip = m_runSourceClip;
    }
}
