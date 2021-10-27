/*
 * Filename: LevelGenBoundaryTests.cs
 * Developer: Zachariah Preston
 * Purpose: Boundary tests for the level generation feature.
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


/*
  Boundary tests for the level generation feature.
 */
public class LevelGenBoundaryTests
{
    /* Tests if the number of rooms in the level is below a lower bound.
     */
    [UnityTest]
    public IEnumerator RoomCountLowerBound()
    {
        // Set the level number.
        LevelGeneration.Inst().SetLevelNum(1);

        // Load scene.
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test for too few rooms.
        GameObject levelGenerator = GameObject.Find("Level Generator");
        Debug.Log("Lower bound test (>= 4) | Level: " + LevelGeneration.Inst().GetLevelNum() + " | Rooms: " +
                  levelGenerator.GetComponent<LevelGeneration>().GetRoomCount());
        Assert.IsTrue(levelGenerator.GetComponent<LevelGeneration>().GetRoomCount() >= 4);
        yield return null;
    }

    /* Tests if the number of rooms in the level is above an upper bound.
     */
    [UnityTest]
    public IEnumerator RoomCountUpperBound()
    {
        // Set the level number.
        LevelGeneration.Inst().SetLevelNum(1);

        // Load scene.
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test for too many rooms.
        GameObject levelGenerator = GameObject.Find("Level Generator");
        Debug.Log("Upper bound test (<= 10) | Level: " + LevelGeneration.Inst().GetLevelNum() + " | Rooms: " +
                  levelGenerator.GetComponent<LevelGeneration>().GetRoomCount());
        Assert.IsTrue(levelGenerator.GetComponent<LevelGeneration>().GetRoomCount() <= 10);
        yield return null;
    }
}

