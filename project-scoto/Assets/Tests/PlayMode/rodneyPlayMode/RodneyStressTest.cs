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
        yield return new WaitForSeconds(5);

        GameObject player = GameObject.Find("Player"); 
        GameObject main_camera = GameObject.FindWithTag("MainCamera");
        Arrow projectile = AssetDatabase.LoadAssetAtPath<Arrow>("Assets/prefabs/rodney/Projectiles/Arrow.prefab");
        Bow bow = GameObject.Find("BowContainer").GetComponent<Bow>();
        player.transform.LookAt(GameObject.Find("HeavyEnemy(Clone)").transform);
        int fire_count = 60000, shots_per_frame = 60;
        Arrow.MAX_TIME = 10000;

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
