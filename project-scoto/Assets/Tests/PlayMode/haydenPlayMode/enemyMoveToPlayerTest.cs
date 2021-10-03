using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
public class EnemyMoveToPlayerTest
{
    [UnityTest]
    public IEnumerator EnemyCloseToPlayer()
    {
        SceneManager.LoadScene("enemyMovementTestScene");
        yield return new WaitForSeconds(5); // wait for scene to load

        GameObject player = GameObject.Find("Player");
        GameObject enemy = GameObject.Find("HeavyEnemy(Clone)");
    
        yield return new WaitForSeconds(5); // wait for enemy to move


        Vector3 playerCoords = player.transform.position;
        Vector3 enemyCoords = enemy.transform.position;
    
        float expectedEnemyDistance = enemy.GetComponent<HeavyEnemy>().GetAttackRange();
        float expectedActualDiff = Mathf.Abs(expectedEnemyDistance - 
                                            Vector3.Distance(playerCoords, enemyCoords));
        
        Assert.IsTrue(expectedActualDiff < 1);

        // see if enemy is in attack range of player
        yield return null;

    }
}
