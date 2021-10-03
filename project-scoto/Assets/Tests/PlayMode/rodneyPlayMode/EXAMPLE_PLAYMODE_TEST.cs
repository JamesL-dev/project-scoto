using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyHealthTest
{
    // private GameObject heavyEnemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>
    //                                      ("Assets/prefabs/hayden/HeavyEnemy.prefab");
    
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
}
