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
        SceneManager.LoadScene("Assets/Tests/PlayMode/jamesPlayMode/jamesTestScene.unity");
        yield return new WaitForSeconds(1.0f);
        var spheres = GameObject.FindGameObjectsWithTag("testSphere");
        //plane = planeCollider.GetComponent<isPlaneTouched>();
        while (true)
        {
            GameObject.Instantiate(spheres[0]);
            yield return new WaitForSeconds(1.0f);
            //Assert.IsTrue(isPlaneTouched);
        }
    }
}
