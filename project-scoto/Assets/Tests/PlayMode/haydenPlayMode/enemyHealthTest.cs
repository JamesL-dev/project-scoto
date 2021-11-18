using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EnemyHealthTest
{
    [UnityTest]
    public IEnumerator EnemyBelow0Health()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(3); // wait for scene to load
        GameObject go = GameObject.Find("Minotaur(clone)");
        if (go == null)
        {
            go = GameObject.Find("Ghoul(clone)");
        }
        Assert.IsTrue(go);
        BaseEnemy baseEnemy = go.GetComponent<BaseEnemy>();

        baseEnemy.TakeDamage(baseEnemy.GetMaxHealth() + 100);
        Assert.AreEqual(0, baseEnemy.GetHealth());

        baseEnemy.TakeHealth(baseEnemy.GetMaxHealth());
        baseEnemy.TakeDamage(baseEnemy.GetMaxHealth());
        Assert.AreEqual(0, baseEnemy.GetHealth());

        baseEnemy.TakeHealth(baseEnemy.GetMaxHealth());
        baseEnemy.TakeDamage(baseEnemy.GetMaxHealth()-1);
        Assert.IsTrue(baseEnemy.GetHealth() > 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnemyAboveMaxHealth()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(3); // wait for scene to load
        GameObject go = GameObject.Find("Minotaur(clone)");
        if (go == null)
        {
            go = GameObject.Find("Ghoul(clone)");
        }
        Assert.IsTrue(go != null);
        BaseEnemy baseEnemy = go.GetComponent<BaseEnemy>();

        baseEnemy.TakeHealth(baseEnemy.GetMaxHealth() + 100);
        Assert.AreEqual(baseEnemy.GetMaxHealth(), baseEnemy.GetHealth());

        baseEnemy.TakeDamage(baseEnemy.GetMaxHealth());
        baseEnemy.TakeHealth(baseEnemy.GetMaxHealth());
        Assert.AreEqual(baseEnemy.GetMaxHealth(), baseEnemy.GetHealth());

        baseEnemy.TakeDamage(1000);
        yield return null;
        Debug.Log(baseEnemy.GetMaxHealth());
        Assert.IsTrue(baseEnemy.GetHealth() < baseEnemy.GetMaxHealth());
        yield return null;
    }
}
