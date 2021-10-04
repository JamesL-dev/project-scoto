using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyHealthTest
{
    private GameObject heavyEnemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>
                                         ("Assets/prefabs/hayden/HeavyEnemy.prefab");
    
    [UnityTest]
    public IEnumerator EnemyBelow0Health()
    {
        HeavyEnemy heavyEnemy = heavyEnemyPrefab.GetComponent<HeavyEnemy>();

        heavyEnemy.TakeDamage(heavyEnemy.GetMaxHealth() + 100);
        Assert.AreEqual(0, heavyEnemy.GetHealth());

        heavyEnemy.TakeHealth(heavyEnemy.GetMaxHealth());
        heavyEnemy.TakeDamage(heavyEnemy.GetMaxHealth());
        Assert.AreEqual(0, heavyEnemy.GetHealth());

        heavyEnemy.TakeHealth(heavyEnemy.GetMaxHealth());
        heavyEnemy.TakeDamage(heavyEnemy.GetMaxHealth()-1);
        Assert.IsTrue(heavyEnemy.GetHealth() > 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnemyAboveMaxHealth()
    {
        HeavyEnemy heavyEnemy = heavyEnemyPrefab.GetComponent<HeavyEnemy>();

        heavyEnemy.TakeHealth(heavyEnemy.GetMaxHealth() + 100);
        Assert.AreEqual(heavyEnemy.GetMaxHealth(), heavyEnemy.GetHealth());

        heavyEnemy.TakeDamage(heavyEnemy.GetMaxHealth());
        heavyEnemy.TakeHealth(heavyEnemy.GetMaxHealth());
        Assert.AreEqual(heavyEnemy.GetMaxHealth(), heavyEnemy.GetHealth());

        heavyEnemy.TakeDamage(1000);
        yield return null;
        Debug.Log(heavyEnemy.GetMaxHealth());
        Assert.IsTrue(heavyEnemy.GetHealth() < heavyEnemy.GetMaxHealth());
        yield return null;
    }
}
