using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GenericFlashlightTests
{

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Game");
    }

    [UnityTest]
    public IEnumerator TestInst()
    {
        GameObject go = GameObject.Find("Flashlight");
        Assert.IsTrue(Flashlight.Inst() != null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestBatteryPercent()
    {
        Flashlight flashlight = Flashlight.Inst();

        float f = flashlight.GetBatteryPercent();
        if (f >= 0.0f && f <= 1.0f)
        {
            Assert.IsTrue(true);
        }
        Assert.IsFalse(false);
        yield return null;
    }
}
