using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FlashlightHealthTest
{

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Game");
    }

    [TearDown]
    public void TearDown()
    {
        SceneManager.UnloadScene("Game");
    }

    [UnityTest]
    public IEnumerator FlashlightAbove100()
    {
        GameObject go = GameObject.Find("Flashlight");
        Flashlight flashlight = go.GetComponent<Flashlight>();
        flashlight.AddBattery(1000000000000000000.0f);
        Assert.AreEqual(1, flashlight.GetBatteryPercent());
        yield return null;
    }
}
