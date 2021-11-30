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
        // Load scene.
        SceneManager.LoadScene("Game");
        LevelGeneration.Inst().SetLevelNum(1);
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test for too few rooms.
        Debug.Log("Room count lower bound test (>= 5) | Level: " + LevelGeneration.Inst().GetLevelNum() + " | Rooms: " +
                  LevelGeneration.Inst().GetRoomCount());
        Assert.IsTrue(LevelGeneration.Inst().GetRoomCount() >= 5);
        yield return null;
    }

    /* Tests if the number of rooms in the level is above an upper bound.
     */
    [UnityTest]
    public IEnumerator RoomCountUpperBound()
    {
        // Load scene.
        SceneManager.LoadScene("Game");
        LevelGeneration.Inst().SetLevelNum(1);
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test for too many rooms.
        Debug.Log("Room count upper bound test (<= 11) | Level: " + LevelGeneration.Inst().GetLevelNum() + " | Rooms: " +
                  LevelGeneration.Inst().GetRoomCount());
        Assert.IsTrue(LevelGeneration.Inst().GetRoomCount() <= 11);
        yield return null;
    }

    /* Tests if the number of rooms in the level is below a lower bound.
     */
    [UnityTest]
    public IEnumerator LevelNumLowerBound()
    {
        // Load scene.
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test level number (far out of bounds).
        LevelGeneration.Inst().SetLevelNum(-99);
        Assert.AreEqual(1, LevelGeneration.Inst().GetLevelNum());
        Debug.Log("Level number lower bound test 1 (>= 1) | Level: " + LevelGeneration.Inst().GetLevelNum());

        // Test level number (barely out of bounds).
        LevelGeneration.Inst().SetLevelNum(0);
        Assert.AreEqual(1, LevelGeneration.Inst().GetLevelNum());
        Debug.Log("Level number lower bound test 2 (>= 1) | Level: " + LevelGeneration.Inst().GetLevelNum());

        // Test level number (within bounds).
        LevelGeneration.Inst().SetLevelNum(1);
        Assert.AreEqual(1, LevelGeneration.Inst().GetLevelNum());
        Debug.Log("Level number lower bound test 3 (>= 1) | Level: " + LevelGeneration.Inst().GetLevelNum());

        yield return null;
    }

    /* Tests if the number of rooms in the level is above an upper bound.
     */
    [UnityTest]
    public IEnumerator LevelNumUpperBound()
    {
        // Load scene.
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test level number (far out of bounds).
        LevelGeneration.Inst().SetLevelNum(999);
        Assert.AreEqual(100, LevelGeneration.Inst().GetLevelNum());
        Debug.Log("Level number upper bound test 1 (<= 100) | Level: " + LevelGeneration.Inst().GetLevelNum());

        // Test level number (barely out of bounds).
        LevelGeneration.Inst().SetLevelNum(101);
        Assert.AreEqual(100, LevelGeneration.Inst().GetLevelNum());
        Debug.Log("Level number upper bound test 1 (<= 100) | Level: " + LevelGeneration.Inst().GetLevelNum());

        // Test level number (within bounds).
        LevelGeneration.Inst().SetLevelNum(100);
        Assert.AreEqual(100, LevelGeneration.Inst().GetLevelNum());
        Debug.Log("Level number upper bound test 1 (<= 100) | Level: " + LevelGeneration.Inst().GetLevelNum());

        yield return null;
    }
}

