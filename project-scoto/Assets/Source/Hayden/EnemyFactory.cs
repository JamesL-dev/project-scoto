/*
 * Filename: EnemyFactory.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the EnemyFactory class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* A EnemyFactory that spawns an enemy of abstract type BaseEnemy
*/
public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject m_lightEnemy;
    [SerializeField] private GameObject m_heavyEnemy;
    private int m_heavySpawnRate;
    private GameObject m_room;


    /*
    * Spawns an enemy
    *
    * Returns:
    * BaseEnemy - abstract base enemy object that was created
    */
    public BaseEnemy SpawnEnemy()
    {
        Vector3 roomSize = m_room.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        int isHeavyEnemySpawn = Random.Range(1, 100);

        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        position.x += Random.Range(-roomSize.x/3, roomSize.x/3);
        position.z += Random.Range(-roomSize.z/3, roomSize.z/3);
        
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(position, out hit, 10f, UnityEngine.AI.NavMesh.AllAreas);

        if (isHeavyEnemySpawn <= m_heavySpawnRate)
        {
            return SpawnHeavyEnemy(hit.position);
        }
        else
        {
            return SpawnLightEnemy(hit.position);
        }
    }

    /*
    * Spawns Light Enemy
    *
    * Returns:
    * LightEnemy - LightEnemy object that was just created
    */
    private LightEnemy SpawnLightEnemy(Vector3 position)
    {
        GameObject go = Instantiate(m_lightEnemy, position, transform.rotation);
        go.transform.parent = transform;
        return go.GetComponent<LightEnemy>();
    }

    /*
    * Spawns Heavy Enemy
    *
    * Returns:
    * HeavyEnemy - HeavyEnemy object that was just created
    */
    private HeavyEnemy SpawnHeavyEnemy(Vector3 position)
    {
        GameObject go = Instantiate(m_heavyEnemy, position, transform.rotation);
        go.transform.parent = transform;
        return go.GetComponent<HeavyEnemy>();
    }

    private void Awake()
    {
        m_heavySpawnRate = 10; // 10%
        m_room = gameObject.transform.parent.gameObject;
    }
}
