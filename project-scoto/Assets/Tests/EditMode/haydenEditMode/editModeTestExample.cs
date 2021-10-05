using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditModeTestExample
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test1()
    {
        int number1 = 5;
        int number2 = 2;
        Assert.AreEqual(number1, number2);
    }

    [Test]
    public void Test2()
    {
        Assert.IsTrue(0 <= 1);
    }

}
