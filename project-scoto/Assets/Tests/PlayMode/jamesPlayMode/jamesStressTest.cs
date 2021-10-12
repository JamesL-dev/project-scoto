using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
public class JamesPlayModeTest
{  
    [UnityTest]
    public IEnumerator jamesStressTest()
    {
        int ballCount = 1;
        
        SceneManager.LoadScene("Assets/Tests/PlayMode/jamesPlayMode/jamesTestScene.unity");
        yield return new WaitForSeconds(2.0f);
        GameObject testSphere = GameObject.FindGameObjectsWithTag("testSphere")[0];
        while (true)
        {
            GameObject.Instantiate(testSphere);
            ballCount++;
            Rigidbody rb = testSphere.GetComponent<Rigidbody>();
            Vector3 v3Velocity = rb.velocity;
            yield return new WaitForSeconds(0.000001f);
            if(testSphere.transform.position.y < 0)
            {
                Debug.Log("Scoin entity out of bounds; Test Fails at " + ballCount + " scoins and spawner velocity of " + v3Velocity);
                break;
            }
            if(1.0f / Time.deltaTime <= 15)
            {
                Debug.Log("Test Fails at " + ballCount + " scoins");
                break;
            }
        }   
    }
}