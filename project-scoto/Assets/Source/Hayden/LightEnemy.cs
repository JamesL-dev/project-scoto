/*
 * Filename: LightEnemy.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the LightEnemy subclass.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LightEnemy is a subclass of abstract BaseEnemy.
 * This class contains implementation on top of BaseEnemy to create a concrete
 * type.
 */
public class LightEnemy : BaseEnemy
{
    protected override void Initialize()
    {
        m_maxHealth = 100.0f;
        m_walkSpeed = 2.0f;
        m_runSpeed = 12.0f;
        m_walkPointWait = 3.0f;
        m_damagePerHit = 10.0f;
        m_attackRange = 4.0f;
        m_sightRange = 20.0f;
        m_attackWait = 0.0f;
    }
    
    protected override void Start()
    {
        base.Start();
        m_walkSource.volume = 0.125f;
        m_walkSource.pitch = 2.0f;

        m_dieSource.volume = 1.0f;
    }
}
