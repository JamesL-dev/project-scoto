using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m_lightEnemy;
    public GameObject m_heavyEnemy;

    private int m_totalEnemySpawnCount;
    private int m_currEnemySpawnCount;
    private int m_heavySpawnRate;
    private LevelGeneration m_levelGenerator;

    void Start()
    {
        m_totalEnemySpawnCount = 1;
        m_currEnemySpawnCount = 0;
        m_heavySpawnRate = 50; // 25%
        m_levelGenerator = GameObject.Find("Level Generator").GetComponent<LevelGeneration>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_currEnemySpawnCount < m_totalEnemySpawnCount)
        {
            SpawnEnemy();
            m_currEnemySpawnCount++;
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
