using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightEnemy : BaseEnemy
{
    protected override void Initialize()
    {
        m_maxHealth = 100.0f;
        m_walkSpeed = 2.0f;
        m_runSpeed = 8.0f;
        m_walkPointWait = 3.0f;
        m_damagePerHit = 10.0f;
        m_attackRange = 4.0f;
        m_sightRange = 10.0f;
        m_attackWait = 0.0f;

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
