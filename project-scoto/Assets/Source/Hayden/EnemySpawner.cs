/*
 * Filename: EnemySpawner.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the EnemySpawner class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An Abstract Factory enemy spawner that spawns a BaseEnemy
   of concrete type (either HeavyEnemy or LightEnemy)
 */
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_lightEnemy;
    [SerializeField] private GameObject m_heavyEnemy;

    private int m_totalEnemyToSpawn;
    private int m_currEnemySpawnCount;
    private int m_heavySpawnRate;
    private LevelGeneration m_levelGenerator;
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
        if (m_currEnemySpawnCount < m_totalEnemyToSpawn)
        {
            SpawnEnemy();
        }
        m_spawnedEnemies = true;
    }

    /*
    * Spawns an enemy
    */
    private void SpawnEnemy()
    {
        int isHeavyEnemySpawn = Random.Range(1, 100);
        if (isHeavyEnemySpawn <= m_heavySpawnRate)
        {
            SpawnHeavyEnemy();
        }
        else
        {
            SpawnLightEnemy();
        }
        m_currEnemySpawnCount++;
    }

    private void Start()
    {
        m_totalEnemyToSpawn = 2;
        m_currEnemySpawnCount = 0;
        m_heavySpawnRate = 25; // 25%
        m_levelGenerator = LevelGeneration.Inst();
        m_spawnedEnemies = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!m_spawnedEnemies)
        {
            SpawnEnemies();
        }
    }

    private void SpawnLightEnemy()
    {
        Instantiate(m_lightEnemy, transform);
    }

    private void SpawnHeavyEnemy()
    {
        Instantiate(m_heavyEnemy, transform);
    }
}
