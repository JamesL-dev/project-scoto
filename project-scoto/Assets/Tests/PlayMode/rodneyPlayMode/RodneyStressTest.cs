using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class RodneyStressTest
{
    // private GameObject heavyEnemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>
    //                                      ("Assets/prefabs/rodney/HeavyEnemy.prefab");
    
    // [UnityTest]
    // public IEnumerator EnemyBelow0Health()
    // {
    //     HeavyEnemy heavyEnemy = heavyEnemyPrefab.GetComponent<HeavyEnemy>();

    //     heavyEnemy.TakeDamage(heavyEnemy.GetMaxHealth() + 100);
    //     Assert.AreEqual(0, heavyEnemy.GetHealth());
    //     yield return null;
    // }

    // [UnityTest]
    // public IEnumerator EnemyAboveMaxHealth()
    // {
    //     HeavyEnemy heavyEnemy = heavyEnemyPrefab.GetComponent<HeavyEnemy>();

    //     heavyEnemy.TakeHealth(heavyEnemy.GetMaxHealth() + 100);
    //     Assert.AreEqual(heavyEnemy.GetMaxHealth(), heavyEnemy.GetHealth());
    //     yield return null;
    // }

    [UnityTest]
    public IEnumerator ArrowStressTest()
    {
        // Fire a large amount of arrows looking away from the heavy enemy. Have the heavy enemy move into them so they collide and get stuck in the enemy.
        // I wasn’t able to get Unity to crash, but I dropped it down to one frame per minute for over 7 minutes before I manually ended the game instance.
        yield return new WaitForSeconds(3);
        GameObject player = GameObject.Find("Player");
        player.transform.LookAt(GameObject.Find("HeavyEnemy").transform);
        player.transform.rotation = new Quaternion(0, player.transform.rotation.eulerAngles.y - 180F, 0, 1);
        yield return new WaitForSeconds(1);
        GameObject.Find("BowContainer").GetComponent<Bow>().Fire(player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(1);
    }
}
