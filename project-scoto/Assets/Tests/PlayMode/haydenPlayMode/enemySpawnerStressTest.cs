using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemySpawnerStressTest
{
    [UnityTest]
    public IEnumerator EnemySpawnerStress()
    {
        SceneManager.LoadScene("enemySpawnerStressScene");
        yield return new WaitForSeconds(3); // wait for scene to load

        GameObject player = GameObject.Find("Player");
        GameObject enemy = GameObject.Find("HeavyEnemy");
        HeavyEnemy enemyScript = enemy.GetComponent<HeavyEnemy>();

        Text enemyTracker = GameObject.Find("NumOfEnemies").GetComponent<Text>();
        long numOfEnemies = 1;

        while (true)
        {
            GameObject newEnemy = GameObject.Instantiate(enemy, enemy.transform.position + new Vector3(0.0f, 0f, 0.0f), enemy.transform.rotation);
            numOfEnemies++;
            enemyTracker.text = "Num: " + numOfEnemies;
            yield return new WaitForSeconds(0.05f);
            
            // check if it breaks

            if (numOfEnemies >= 300)
            {
                break;
            }
        }

        Assert.IsTrue(numOfEnemies >= 300);

        yield return null;

    }
}
