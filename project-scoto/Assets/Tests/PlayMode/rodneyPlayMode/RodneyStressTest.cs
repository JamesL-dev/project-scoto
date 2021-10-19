/*
 * Filename: RodneyStressTest.cs
 * Developer: Rodney McCoy
 * Purpose: Stress Test
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/*
 * Main Stress Test Class
 */
public class RodneyStressTest : MonoBehaviour
{
    public static bool m_testSucceeded;
    public static int m_shots, m_totalArrows;
    public static GameObject m_player, m_mainCamera, m_projectile;

    [UnityTest]
    public IEnumerator ArrowStressTest()
    {   
        SceneManager.LoadScene("RodneyStressTest");
        yield return new WaitForSeconds(1);

        m_player = GameObject.Find("Player"); 
        m_mainCamera = GameObject.FindWithTag("MainCamera");
        m_projectile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/rodney/Projectiles/Arrow.prefab");
        float fps_init = (1.0f / Time.smoothDeltaTime);
        m_testSucceeded = false;
        m_shots = 1500;
        yield return null;

        TestFireArrow();
        yield return new WaitForSeconds(2F);
        for(m_totalArrows = 1; m_totalArrows < m_shots; m_totalArrows++) 
        {
            TestFireArrow();
            if(Time.smoothDeltaTime > 0.2F) {m_testSucceeded = true; Debug.Log("[RodneyStressTest.cs -- ArrowStressTest()] Stress test succeeded: framerate dropped below threshold");}
            if(m_testSucceeded) break;
            yield return null;
        }

        Debug.Log("Initial FPS [" + fps_init + "],  Final FPS [" + (1.0f / Time.smoothDeltaTime) + "],  Total Arrows Fired [" + m_totalArrows + "]");
        yield return new WaitForSeconds(.25F);
        if(m_totalArrows == 1) {Assert.IsTrue(false); Debug.Log("[RodneyStressTest.cs -- ArrowStressTest()] Stress test failed: first arrow failed, implying it flew straight through the enemy");}
        Assert.IsTrue(m_testSucceeded); 
    }

    public void TestFireArrow()
    {
        m_player.transform.LookAt(GameObject.Find("HeavyEnemy(Clone)").transform);
        Quaternion rotation = m_mainCamera.transform.rotation * Quaternion.Euler(-90,0,0);
        GameObject proj_edit = Instantiate(m_projectile.gameObject, m_mainCamera.transform.position - rotation*Vector3.up, rotation) as GameObject;
        proj_edit.AddComponent<ArrowTest>();
        proj_edit.GetComponent<Arrow>().Test();
        proj_edit.name = "Arrow " + m_totalArrows;
    }
}
