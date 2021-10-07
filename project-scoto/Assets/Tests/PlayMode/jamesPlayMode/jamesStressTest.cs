using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
public class JamesPlayModeTest
{
    // [SetUp]
    // void setupFunction()
    // {
    //     SceneManager.LoadScene("Assets/Tests/PlayMode/jamesPlayMode/jamesTestScene.unity");
    // }
    
    [UnityTest]
    public IEnumerator checkPowerupBounds()
    {
        int ballCount = 1;
        
        SceneManager.LoadScene("Assets/Tests/PlayMode/jamesPlayMode/jamesTestScene.unity");
        yield return new WaitForSeconds(2.0f);
        GameObject testSphere = GameObject.FindGameObjectsWithTag("testSphere")[0];
        while (true)
        {
            GameObject.Instantiate(testSphere);
            ballCount++;
            yield return new WaitForSeconds(0.000001f);
            if(testSphere.transform.position.y < 0)
            {
                Debug.Log("Scoin entity out of bounds");
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