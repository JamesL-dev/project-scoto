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
    float fps = 0;

    [UnityTest]
    public IEnumerator EnemySpawnerStress()
    {
        SceneManager.LoadScene("enemySpawnerStressScene");
        yield return new WaitForSeconds(3); // wait for scene to load

        GameObject player = GameObject.Find("Player");
        GameObject enemy = GameObject.Find("Minotaur");
        HeavyEnemy enemyScript = enemy.GetComponent<HeavyEnemy>();

        Text enemyTracker = GameObject.Find("NumOfEnemies").GetComponent<Text>();
        long numOfEnemies = 1;

        while (true)
        {
            fps = 1.0f / Time.deltaTime;
            GameObject newEnemy = GameObject.Instantiate(enemy, enemy.transform.position + new Vector3(0.0f, 0f, 0.0f), enemy.transform.rotation);
            newEnemy.transform.parent = enemy.transform.parent;
            numOfEnemies++;
            enemyTracker.text = "Num: " + numOfEnemies + " FPS: " + fps;
            
            // check if it breaks

            if (fps < 15)
            {
                Debug.Log("Number of enemies when fps dipped below 15: " + numOfEnemies);
                break;

            }

            yield return null; // wait a frame

        }
        yield return null;

    }
}
