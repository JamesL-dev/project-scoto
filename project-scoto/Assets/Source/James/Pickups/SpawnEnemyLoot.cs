using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyLoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void SpawnLoot()
    {
        // // Drop Some Loot
        // for (int i = 0; i < 4; i++)
        // {
        //     var go = Instantiate(m_HealthOrbPrefab, transform.position + new Vector3(0, Random.Range(0, 2)), Quaternion.identity);

        //     go.GetComponent<Follow>().m_target = m_DropLootTracker.transform; // Target the players loot tracker
        // }
        Debug.Log("Loot Spawned");
    }
}
