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

        Vector3 roomSize = m_roomIn.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_walkPointRange = Mathf.Min(roomSize.x, roomSize.z) * 0.5f * 0.75f;

    }

    public override void AlertObservers(string message)
    {
        Debug.Log("alert observers is called!");
        if (message.Equals("AttackAnimationEnded"))
        {
            if (m_playerInAttackRange)
            {
                // player.TakeDamage(m_damagePerHit);
            }
            
        }

        if (message.Equals("DeathAnimationEnded"))
        {
            GameObject.Destroy(gameObject, 1.0f);
            SpawnEnemyLoot.SpawnLoot();
        }
    }
}
