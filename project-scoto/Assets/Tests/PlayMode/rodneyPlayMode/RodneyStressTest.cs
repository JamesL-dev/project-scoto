using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;
//using UnityEngine.Time;

public class RodneyStressTest : MonoBehaviour
{
    public static bool TestSucceeded;
    public const int frames = 1800, shots_per_frame = 1;
    [UnityTest]
    public IEnumerator ArrowStressTest()
    {   
        SceneManager.LoadScene("RodneyStressTest");
        yield return new WaitForSeconds(3);

        GameObject player = GameObject.Find("Player"); 
        GameObject main_camera = GameObject.FindWithTag("MainCamera");
        GameObject projectile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/rodney/Projectiles/Arrow.prefab");

        player.transform.LookAt(GameObject.Find("HeavyEnemy(Clone)").transform);
        float fps_init = (1.0f / Time.smoothDeltaTime);
        TestSucceeded = false;
        Quaternion rotation = main_camera.transform.rotation * Quaternion.Euler(-90,0,0);
        Vector3 position = main_camera.transform.position - rotation*Vector3.up;

        TestFireArrow(projectile, position, rotation);
        yield return new WaitForSeconds(1.5F);
        for(int i = 0; i < frames; i++) 
        {
            for(int j = 0; j < shots_per_frame; j++)
            {
                TestFireArrow(projectile, position, rotation);
                if(TestSucceeded) break;
            }
            yield return null;
            if(TestSucceeded) break;
        }
        Debug.Log("Initial FPS: " + fps_init + " | Final FPS: " + (1.0f / Time.smoothDeltaTime));
        yield return new WaitForSeconds(3F);
        Assert.IsTrue(TestSucceeded); 
    }

    public void TestFireArrow(GameObject projectile, Vector3 position, Quaternion rotation)
    {
        GameObject proj_edit = Instantiate(projectile.gameObject, position, rotation) as GameObject;
        proj_edit.AddComponent<ArrowTest>();
        Arrow proj_script = proj_edit.GetComponent<Arrow>();
        proj_script.MAX_TIME = frames + 10000;
        proj_script.velocity_scalar = 3F;
    }
}
