using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public LightEnemy m_lightEnemy;

    private int m_totalEnemySpawnCount;
    private int m_currEnemySpawnCount;
    private LevelGeneration m_levelGenerator;

    void Start()
    {
        m_totalEnemySpawnCount = 1;
        m_currEnemySpawnCount = 0;
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
        Instantiate(m_lightEnemy, transform);
    }
}
