using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class RodneyStressTest
{
    // private GameObject heavyEnemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>
    //                                      ("Assets/prefabs/rodney/HeavyEnemy.prefab");
    
    private GameObject player = GameObject.FindWithTag("Player"); 
    //AssetDatabase.LoadAssetAtPath<GameObject>("/Game/Player");
    private Bow bow = GameObject.Find("BowContainer").GetComponent<Bow>();

    [UnityTest]
    public IEnumerator ArrowStressTest()
    {
        // Fire a large amount of arrows looking away from the heavy enemy. Have the heavy enemy move into them so they collide and get stuck in the enemy.
        // I wasn’t able to get Unity to crash, but I dropped it down to one frame per minute for over 7 minutes before I manually ended the game instance.
        int fire_count = 1000;
        int shots_per_frame = 10;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(5);
        player.transform.LookAt(GameObject.Find("HeavyEnemy").transform);
        //player.transform.rotation = new Quaternion(0, player.transform.rotation.eulerAngles.y - 180F, 0, 1);
        yield return new WaitForSeconds(1);
        for(int i = 0; i < fire_count/shots_per_frame; i++) 
        {
            for(int j = 0; j < shots_per_frame; j++)
            {
                bow.Fire(player.transform.position, player.transform.rotation); yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
