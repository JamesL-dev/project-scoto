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
    [SerializeField] private AudioClip m_roarSourceClip;

    private AudioSource m_roarSource;
    private float m_roarWait;
    private bool m_roarWaiting;

    protected override void Initialize()
    {
        m_maxHealth = 300.0f;
        m_walkSpeed = 1.5f;
        m_runSpeed = 6.0f;
        m_walkPointWait = 5.0f;
        m_damagePerHit = 20.0f;
        m_attackRange = 2.0f;
        m_sightRange = 20.0f;
        m_attackWait = 2.0f;
        m_roarWait = 15.0f; 

        m_roarWaiting = false;

        m_roarSource = gameObject.AddComponent<AudioSource>();
        m_roarSource.clip = m_roarSourceClip;
        m_roarSource.volume = 0.25f; 
        m_roarSource.spatialBlend = 1.0f;
        m_roarSource.maxDistance = 25.0f;
        m_roarSource.rolloffMode = AudioRolloffMode.Linear;

        Vector3 roomSize = m_roomIn.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_walkPointRange = Mathf.Min(roomSize.x, roomSize.z) * 0.5f * 0.75f;
    }

    private void Roar()
    {
        m_state = EnemyState.Roaring;
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);
        m_agent.speed = 0;
        m_agent.SetDestination(transform.position);
    }

    protected override void ChasePlayer()
    {
        // if need to start roaring
        if ((m_state == EnemyState.Idle || m_state == EnemyState.Walking) && !m_roarWaiting)
        {
            m_roarWaiting = true;
            HaydenHelpers.StartClock(m_roarWait, () => m_roarWaiting = false);
            Roar();
            return;
        }
        else if (m_state == EnemyState.Roaring) // currently roaring
        {
            Roar();
        }
        else // not roaring, and dont need to roar
        {
            base.ChasePlayer();
        }
    }

    protected override void DoAnimations()
    {
        base.DoAnimations();
        switch(m_state)
        {
            case EnemyState.Roaring:
                HaydenHelpers.SetAnimation(m_animator, "isRoaring");
                break;
        }
    }

    protected override void AlertObservers(string message)
    {
        base.AlertObservers(message);

        if (message.Equals("RoarStarted"))
        {
            m_roarSource.Play();
        }

        if (message.Equals("RoarAnimationEnded"))
        {
            m_state = EnemyState.Idle;
        }
    }
}
