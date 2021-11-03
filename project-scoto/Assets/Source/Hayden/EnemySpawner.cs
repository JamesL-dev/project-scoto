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
    private float m_enemyDensity;
    private float m_levelDensityMultiplier;
    private LevelGeneration m_levelGenerator;
    private GameObject m_room;
    private bool m_spawnedEnemies;
    private bool m_enemyDefeated;

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
        m_enemyDefeated = true;
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

    /* Returns true if 1 enemy was defeated, false otherwise
    * Returns:
    * bool - true if 1 enemy was defeated, false otherwise
    */
    public bool EnemyDefeated()
    {
        return m_enemyDefeated;
    }

    /*
    * Spawns an indeterminate amount of enemies
    */
    public void SpawnEnemies()
    {
        while (m_currEnemySpawnCount < m_totalEnemyToSpawn)
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

    private void CalculateTotalEnemySpawn()
    {
        Vector3 roomSize = m_room.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_levelDensityMultiplier = 1.0f + LevelGeneration.Inst().GetLevelNum() / 5.0f; // doubles every 5 levels
        float roomSizeF = roomSize.x * roomSize.z;
        m_totalEnemyToSpawn = (int) (m_enemyDensity * roomSizeF * m_levelDensityMultiplier);
    }

    private void Start()
    {
        m_enemyDefeated = false;
        m_room = gameObject.transform.parent.gameObject;
        m_currEnemySpawnCount = 0;
        m_heavySpawnRate = 25; // 25%
        m_levelGenerator = LevelGeneration.Inst();
        m_spawnedEnemies = false;
        m_enemyDensity = 0.0025f;
        m_levelDensityMultiplier = 2.0f;

        CalculateTotalEnemySpawn();

        if (m_totalEnemyToSpawn == 0)
        {
            m_enemyDefeated = true;
        }
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

    private void SpawnLightEnemy()
    {
        Instantiate(m_lightEnemy, transform);
    }

    private void SpawnHeavyEnemy()
    {
        Instantiate(m_heavyEnemy, transform);
    }
}
