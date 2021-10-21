/*
* Filename: boundaryTestOne.cs
* Developer: James Lasso
* Purpose: Boundary Tests
*/
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/*
* Runs boundary tests
*
* Member variables: none
*/
public class boundaryTests
{
    /* Function to check the upper bounds of a scoin value.
    *
    * Parameters: none
    *
    * Returns:
    */
    [UnityTest]
    public IEnumerator powerUpAmountUpperBound()
    {
        SceneManager.LoadScene("Assets/Tests/PlayMode/jamesPlayMode/jamesBoundaryTest.unity");
        yield return new WaitForSeconds(2.0f);

        GameObject scoinObj = new GameObject();
        scoinObj.AddComponent<Scoin>();
        Scoin scoin = scoinObj.GetComponent<Scoin>();

        scoin.scoinBonus = 5;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 5);

        scoin.scoinBonus = 4;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 4);
        

        scoin.scoinBonus = 6;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 5);
        

        scoin.scoinBonus = 50;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 5);
    }

    /* Function to check the lower bounds of scoin value.
    *
    * Parameters: none
    *
    * Returns:
    */
    [UnityTest]
    public IEnumerator powerUpAmountLowerBound()
    {
        SceneManager.LoadScene("Assets/Tests/PlayMode/jamesPlayMode/jamesBoundaryTest.unity");
        yield return new WaitForSeconds(2.0f);

        GameObject scoinObj = new GameObject();
        scoinObj.AddComponent<Scoin>();
        Scoin scoin = scoinObj.GetComponent<Scoin>();

        scoin.scoinBonus = 1;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 1);
        

        scoin.scoinBonus = 2;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 2);
        

        scoin.scoinBonus = 0;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 1);
        

        scoin.scoinBonus = -10;
        yield return null;
        Debug.Log(scoin.scoinBonus);
        Assert.AreEqual(scoin.scoinBonus, 1);
    }
    
}