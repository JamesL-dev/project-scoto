// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using UnityEditor;
// using UnityEngine.SceneManagement;
// public class PlayModeTestExample
// {
//     // checks if enemy moves to player properly
//     [UnityTest]
//     public IEnumerator EnemyCloseToPlayer()
//     {
//         SceneManager.LoadScene("enemyMovementTestScene"); // loads a test scene
//         yield return new WaitForSeconds(2); // wait for scene to load

//         GameObject player = GameObject.Find("Player"); // find the Player in the scene
//         GameObject enemy = GameObject.Find("HeavyEnemy(Clone)"); // find the enemy in the scene
    
//         yield return new WaitForSeconds(5); // wait for enemy to move

//         Vector3 playerCoords = player.transform.position;
//         Vector3 enemyCoords = enemy.transform.position;
    
//         float expectedEnemyDistance = enemy.GetComponent<HeavyEnemy>().GetAttackRange();
//         float expectedActualDiff = Mathf.Abs(expectedEnemyDistance - 
//                                             Vector3.Distance(playerCoords, enemyCoords));
        
//         Assert.IsTrue(expectedActualDiff < 1); // allowing for some discrepancy

//         yield return null;

//     }
// }
