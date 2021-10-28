using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m_lightEnemy;
    public GameObject m_heavyEnemy;

    private int m_totalEnemyToSpawn;
    private int m_currEnemySpawnCount;
    private int m_heavySpawnRate;
    private LevelGeneration m_levelGenerator;

    void Start()
    {
        m_totalEnemyToSpawn = 1;
        m_currEnemySpawnCount = 0;
        m_heavySpawnRate = 25; // 25%
        m_levelGenerator = LevelGeneration.Inst();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_currEnemySpawnCount < m_totalEnemyToSpawn)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
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

    private void SpawnLightEnemy()
    {
        Instantiate(m_lightEnemy, transform);
    }

    private void SpawnHeavyEnemy()
    {
        Instantiate(m_heavyEnemy, transform);
    }

    public void DecrementEnemy()
    {
        m_currEnemySpawnCount--;

        if (m_currEnemySpawnCount < 0)
        {
            m_currEnemySpawnCount = 0;
        }
    }

    public int GetEnemyCount()
    {
        return m_currEnemySpawnCount;
    }
}
