using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeavyEnemy : BaseEnemy
{
    protected override void Initialize()
    {
        m_maxHealth = 300.0f;
        m_walkSpeed = 1.0f;
        m_runSpeed = 4.0f;
        m_walkPointWait = 5.0f;
        m_damagePerHit = 20.0f;

        Vector3 roomSize = m_roomIn.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_walkPointRange = Mathf.Min(roomSize.x, roomSize.z) * 0.5f * 0.75f;
    }
}
