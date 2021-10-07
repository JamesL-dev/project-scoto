using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class RodneyStressTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator ArrowStressTest()
    {   
        SceneManager.LoadScene("RodneyStressTest");
        yield return new WaitForSeconds(8);

        GameObject player = GameObject.FindWithTag("Player"); 
        GameObject main_camera = GameObject.FindWithTag("MainCamera");
        GameObject projectile = GameObject.Find("Arrow");
        Bow bow = GameObject.Find("BowContainer").GetComponent<Bow>();
        int fire_count = 1000, shots_per_frame = 10;

        player.transform.LookAt(GameObject.Find("HeavyEnemy").transform);
        yield return new WaitForSeconds(.5F);

        Quaternion rotation = main_camera.transform.rotation * Quaternion.Euler(-90,0,0);
        for(int i = 0; i < fire_count/shots_per_frame; i++) 
        {
            for(int j = 0; j < shots_per_frame; j++)
            {
                Instantiate(projectile, main_camera.transform.position - rotation*Vector3.up, rotation);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
