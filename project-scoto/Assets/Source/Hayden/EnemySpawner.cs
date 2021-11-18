/*
 * Filename: EnemySpawner.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the EnemySpawner class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * A enemy spawner that spawns the correct # of enemies for a room
 */
public class EnemySpawner : MonoBehaviour
{
    private int m_totalEnemyToSpawn;
    private int m_currEnemySpawnCount;
    private float m_enemyDensity;
    private float m_levelDensityMultiplier;
    private LevelGeneration m_levelGenerator;
    private EnemyFactory m_enemyFactory;
    private GameObject m_room;
    private bool m_spawnedEnemies;

    /*
    * This function is only to be used by a BaseEnemy instance. On the death of
    * an enemy, the enemy will call this function to decrement the total count
    * of enemies.
    */
    public void DecrementEnemy()
    {
        m_currEnemySpawnCount--;

        if (m_currEnemySpawnCount < 0)
        {
            m_currEnemySpawnCount = 0;
        }
    }

    /*
    * Gets the current count of how many enemies are still alive that were 
    * spawned by the spawner
    *
    * Returns:
    * int - number of enemies still alive
    */
    public int GetEnemyCount()
    {
        return m_currEnemySpawnCount;
    }

    /*
    * Spawns an indeterminate amount of enemies
    */
    public void SpawnEnemies()
    {
        if (!m_levelGenerator.GetIsBaked())
        {
            return;
        }
        while (m_currEnemySpawnCount < m_totalEnemyToSpawn)
        {
            m_enemyFactory.SpawnEnemy();
            m_currEnemySpawnCount++;

        }
        m_spawnedEnemies = true;
    }

    private void CalculateTotalEnemySpawn()
    {
        Vector3 roomSize = m_room.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_levelDensityMultiplier = 1.0f + LevelGeneration.Inst().GetLevelNum() / 15.0f; // doubles every 15 levels
        float roomSizeF = roomSize.x * roomSize.z;
        m_totalEnemyToSpawn = (int) (m_enemyDensity * roomSizeF * m_levelDensityMultiplier);
    }

    private void Start()
    {
        m_room = gameObject.transform.parent.gameObject;
        m_enemyFactory = gameObject.GetComponent<EnemyFactory>();
        m_currEnemySpawnCount = 0;
        m_levelGenerator = LevelGeneration.Inst();
        m_spawnedEnemies = false;
        m_enemyDensity = 0.0025f;
        m_levelDensityMultiplier = 2.0f;

        CalculateTotalEnemySpawn();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!m_spawnedEnemies)
        {
            SpawnEnemies();
        }

        CalculateTotalEnemySpawn();
    }
}
