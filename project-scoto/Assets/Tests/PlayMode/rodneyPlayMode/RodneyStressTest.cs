using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class RodneyStressTest : MonoBehaviour
{
    public static bool TestSucceeded;
    public static int shots, total_arrows;

    public static GameObject player, main_camera, projectile;

    [UnityTest]
    public IEnumerator ArrowStressTest()
    {   
        SceneManager.LoadScene("RodneyStressTest");
        yield return new WaitForSeconds(1);

        player = GameObject.Find("Player"); 
        main_camera = GameObject.FindWithTag("MainCamera");
        projectile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/rodney/Projectiles/Arrow.prefab");
        float fps_init = (1.0f / Time.smoothDeltaTime);
        TestSucceeded = false;
        shots = 1500;
        yield return null;

        TestFireArrow();
        yield return new WaitForSeconds(2F);
        for(total_arrows = 1; total_arrows < shots; total_arrows++) 
        {
            TestFireArrow();
            if(Time.smoothDeltaTime > 0.2F) {TestSucceeded = true; Debug.Log("Test ended because framerate dropped below threshold");}
            if(TestSucceeded) break;
            yield return null;
        }

        Debug.Log("Initial FPS [" + fps_init + "],  Final FPS [" + (1.0f / Time.smoothDeltaTime) + "],  Total Arrows Fired [" + total_arrows + "]");
        yield return new WaitForSeconds(.25F);
        if(total_arrows == 1) {Assert.IsTrue(false); Debug.Log("Test ended because first arrow failed.");}
        Assert.IsTrue(TestSucceeded); 
    }

    public void TestFireArrow()
    {
        player.transform.LookAt(GameObject.Find("HeavyEnemy(Clone)").transform);
        Quaternion rotation = main_camera.transform.rotation * Quaternion.Euler(-90,0,0);
        GameObject proj_edit = Instantiate(projectile.gameObject, main_camera.transform.position - rotation*Vector3.up, rotation) as GameObject;
        proj_edit.AddComponent<ArrowTest>();
        proj_edit.GetComponent<Arrow>().Test();
        proj_edit.name = "Arrow " + total_arrows;
    }
}
