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
            SpawnEnemy();
        }
        m_spawnedEnemies = true;
    }

    /*
    * Spawns an enemy
    */
    private void SpawnEnemy()
    {
        Vector3 roomSize = m_room.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        int isHeavyEnemySpawn = Random.Range(1, 100);

        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        position.x += Random.Range(-roomSize.x/3, roomSize.x/3);
        position.z += Random.Range(-roomSize.z/3, roomSize.z/3);
        
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 10f, NavMesh.AllAreas);

        if (isHeavyEnemySpawn <= m_heavySpawnRate)
        {
            SpawnHeavyEnemy(hit.position);
        }
        else
        {
            SpawnLightEnemy(hit.position);
        }
        m_currEnemySpawnCount++;
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
        m_currEnemySpawnCount = 0;
        m_heavySpawnRate = 25; // 25%
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

    private void SpawnLightEnemy(Vector3 position)
    {
        GameObject go = Instantiate(m_lightEnemy, position, transform.rotation);
        go.transform.parent = transform;
    }

    private void SpawnHeavyEnemy(Vector3 position)
    {
        GameObject go = Instantiate(m_heavyEnemy, position, transform.rotation);
        go.transform.parent = transform;
    }
}
